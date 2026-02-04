using AutoMapper;
using CB.Application.DTOs.VirtualEducationCaption;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class VirtualEducationCaptionService : IVirtualEducationCaptionService
    {
        private readonly IGenericRepository<VirtualEducationCaption> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public VirtualEducationCaptionService(
            IMapper mapper,
            IGenericRepository<VirtualEducationCaption> repository,
            IGenericRepository<Language> languageRepository,
            IWebHostEnvironment env
        )
        {
            _mapper = mapper;
            _repository = repository;
            _languageRepository = languageRepository;
            _env = env;
        }

        public async Task<bool> CreateOrUpdate(VirtualEducationCaptionPostDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            VirtualEducationCaption? financialDevelopment = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync();

            bool result;

            if (financialDevelopment is null)
            {
                financialDevelopment = _mapper.Map<VirtualEducationCaption>(dto);
                if (dto.File != null) financialDevelopment.Image = await dto.File.FileUpload(_env.WebRootPath, "virtual-education-captions");


                financialDevelopment.Translations = dto.Titles.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    return new VirtualEducationCaptionTranslation
                    {
                        LanguageId = lang.Id,
                        Title = v.Value,
                    };
                }).ToList();

                result = await _repository.AddAsync(financialDevelopment);
            }
            else
            {
                _mapper.Map(dto, financialDevelopment);

                if (dto.File != null)
                {
                    FileManager.FileDelete(_env.WebRootPath, financialDevelopment.Image ?? "");
                    financialDevelopment.Image = await dto.File.FileUpload(_env.WebRootPath, "virtual-education-captions");
                }

                foreach (var v in dto.Titles)
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");


                    var existingTranslation = financialDevelopment.Translations.FirstOrDefault(t => t.LanguageId == lang.Id);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Title = v.Value;
                    }
                    else
                    {
                        financialDevelopment.Translations.Add(new VirtualEducationCaptionTranslation
                        {
                            LanguageId = lang.Id,
                            Title = v.Value,
                        });
                    }
                }

                result = await _repository.UpdateAsync(financialDevelopment);
            }


            return result;
        }

        public async Task<VirtualEducationCaptionGetDTO?> GetFirst()
        {
            VirtualEducationCaption? financialDevelopment = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync(h => h.Id == 1);

            return financialDevelopment == null ? null : _mapper.Map<VirtualEducationCaptionGetDTO>(financialDevelopment);
        }
    }
}
