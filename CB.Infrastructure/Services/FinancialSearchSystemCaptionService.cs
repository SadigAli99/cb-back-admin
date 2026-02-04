using AutoMapper;
using CB.Application.DTOs.FinancialSearchSystemCaption;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class FinancialSearchSystemCaptionService : IFinancialSearchSystemCaptionService
    {
        private readonly IGenericRepository<FinancialSearchSystemCaption> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public FinancialSearchSystemCaptionService(
            IMapper mapper,
            IGenericRepository<FinancialSearchSystemCaption> repository,
            IGenericRepository<Language> languageRepository,
            IWebHostEnvironment env
        )
        {
            _mapper = mapper;
            _repository = repository;
            _languageRepository = languageRepository;
            _env = env;
        }

        public async Task<bool> CreateOrUpdate(FinancialSearchSystemCaptionPostDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            FinancialSearchSystemCaption? financialDevelopment = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync();

            bool result;

            if (financialDevelopment is null)
            {
                financialDevelopment = _mapper.Map<FinancialSearchSystemCaption>(dto);
                if (dto.File != null) financialDevelopment.Image = await dto.File.FileUpload(_env.WebRootPath, "financial-search-system-captions");


                financialDevelopment.Translations = dto.Titles.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    return new FinancialSearchSystemCaptionTranslation
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
                    financialDevelopment.Image = await dto.File.FileUpload(_env.WebRootPath, "financial-search-system-captions");
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
                        financialDevelopment.Translations.Add(new FinancialSearchSystemCaptionTranslation
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

        public async Task<FinancialSearchSystemCaptionGetDTO?> GetFirst()
        {
            FinancialSearchSystemCaption? financialDevelopment = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync(h => h.Id == 1);

            return financialDevelopment == null ? null : _mapper.Map<FinancialSearchSystemCaptionGetDTO>(financialDevelopment);
        }
    }
}
