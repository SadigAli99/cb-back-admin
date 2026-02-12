using AutoMapper;
using CB.Application.DTOs.FinancialLiteracyPortalCaption;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class FinancialLiteracyPortalCaptionService : IFinancialLiteracyPortalCaptionService
    {
        private readonly IGenericRepository<FinancialLiteracyPortalCaption> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public FinancialLiteracyPortalCaptionService(
            IGenericRepository<FinancialLiteracyPortalCaption> repository,
            IGenericRepository<Language> languageRepository,
            IFileService fileService,
            IMapper mapper
        )
        {
            _languageRepository = languageRepository;
            _fileService = fileService;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> CreateOrUpdate(FinancialLiteracyPortalCaptionPostDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            FinancialLiteracyPortalCaption? financialDevelopment = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync();

            bool result;

            if (financialDevelopment is null)
            {
                financialDevelopment = _mapper.Map<FinancialLiteracyPortalCaption>(dto);
                if (dto.File != null) financialDevelopment.Image = await _fileService.UploadAsync(dto.File, "financial-literacy-portal-captions");


                financialDevelopment.Translations = dto.Titles.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    return new FinancialLiteracyPortalCaptionTranslation
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
                    _fileService.Delete(financialDevelopment.Image ?? "");
                    financialDevelopment.Image = await _fileService.UploadAsync(dto.File, "financial-literacy-portal-captions");
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
                        financialDevelopment.Translations.Add(new FinancialLiteracyPortalCaptionTranslation
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

        public async Task<FinancialLiteracyPortalCaptionGetDTO?> GetFirst()
        {
            FinancialLiteracyPortalCaption? financialDevelopment = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync(h => h.Id == 1);

            return financialDevelopment == null ? null : _mapper.Map<FinancialLiteracyPortalCaptionGetDTO>(financialDevelopment);
        }
    }
}
