using AutoMapper;
using CB.Application.DTOs.FinancialDevelopment;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class FinancialDevelopmentService : IFinancialDevelopmentService
    {
        private readonly IGenericRepository<FinancialDevelopment> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public FinancialDevelopmentService(
            IMapper mapper,
            IGenericRepository<FinancialDevelopment> repository,
            IGenericRepository<Language> languageRepository,
            IWebHostEnvironment env
        )
        {
            _mapper = mapper;
            _repository = repository;
            _languageRepository = languageRepository;
            _env = env;
        }

        public async Task<bool> CreateOrUpdate(FinancialDevelopmentPostDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            FinancialDevelopment? financialDevelopment = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync();

            bool result;

            if (financialDevelopment is null)
            {
                financialDevelopment = _mapper.Map<FinancialDevelopment>(dto);
                if (dto.File != null) financialDevelopment.PdfFile = await dto.File.FileUpload(_env.WebRootPath, "financial-developments");
                if (dto.ImageFile != null) financialDevelopment.Image = await dto.ImageFile.FileUpload(_env.WebRootPath, "financial-developments");


                financialDevelopment.Translations = dto.Titles.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    dto.Descriptions.TryGetValue(v.Key, out var description);
                    dto.FileHeadTitles.TryGetValue(v.Key, out var fileHeadTitle);
                    dto.FileTitles.TryGetValue(v.Key, out var fileTitle);

                    return new FinancialDevelopmentTranslation
                    {
                        LanguageId = lang.Id,
                        Title = v.Value,
                        Description = description,
                        FileHeadTitle = fileHeadTitle,
                        FileTitle = fileTitle
                    };
                }).ToList();

                result = await _repository.AddAsync(financialDevelopment);
            }
            else
            {
                _mapper.Map(dto, financialDevelopment);
                if (dto.File != null)
                {
                    FileManager.FileDelete(_env.WebRootPath, financialDevelopment.PdfFile ?? "");
                    financialDevelopment.PdfFile = await dto.File.FileUpload(_env.WebRootPath, "financial-developments");
                }

                if (dto.ImageFile != null)
                {
                    FileManager.FileDelete(_env.WebRootPath, financialDevelopment.Image ?? "");
                    financialDevelopment.Image = await dto.ImageFile.FileUpload(_env.WebRootPath, "financial-developments");
                }

                foreach (var v in dto.Titles)
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    dto.Descriptions.TryGetValue(v.Key, out var description);
                    dto.FileHeadTitles.TryGetValue(v.Key, out var fileHeadTitle);
                    dto.FileTitles.TryGetValue(v.Key, out var fileTitle);


                    var existingTranslation = financialDevelopment.Translations?.FirstOrDefault(t => t.LanguageId == lang.Id);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Title = v.Value;
                        existingTranslation.Description = description;
                        existingTranslation.FileHeadTitle = fileHeadTitle;
                        existingTranslation.FileTitle = fileTitle;
                    }
                    else
                    {
                        financialDevelopment.Translations.Add(new FinancialDevelopmentTranslation
                        {
                            LanguageId = lang.Id,
                            Title = v.Value,
                            Description = description,
                            FileHeadTitle = fileHeadTitle,
                            FileTitle = fileTitle
                        });
                    }
                }

                result = await _repository.UpdateAsync(financialDevelopment);
            }


            return result;
        }

        public async Task<FinancialDevelopmentGetDTO?> GetFirst()
        {
            FinancialDevelopment financialDevelopment = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync(h => h.Id == 1);

            return financialDevelopment == null ? null : _mapper.Map<FinancialDevelopmentGetDTO>(financialDevelopment);
        }
    }
}
