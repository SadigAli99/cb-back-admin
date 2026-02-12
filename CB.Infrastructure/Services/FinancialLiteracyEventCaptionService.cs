using AutoMapper;
using CB.Application.DTOs.FinancialLiteracyEventCaption;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class FinancialLiteracyEventCaptionService : IFinancialLiteracyEventCaptionService
    {
        private readonly IGenericRepository<FinancialLiteracyEventCaption> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public FinancialLiteracyEventCaptionService(
            IGenericRepository<FinancialLiteracyEventCaption> repository,
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

        public async Task<bool> CreateOrUpdate(FinancialLiteracyEventCaptionPostDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            FinancialLiteracyEventCaption? financialDevelopment = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync();

            bool result;

            if (financialDevelopment is null)
            {
                financialDevelopment = _mapper.Map<FinancialLiteracyEventCaption>(dto);
                if (dto.File != null) financialDevelopment.Image = await _fileService.UploadAsync(dto.File, "financial-literacy-event-captions");


                financialDevelopment.Translations = dto.Titles.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    return new FinancialLiteracyEventCaptionTranslation
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
                    financialDevelopment.Image = await _fileService.UploadAsync(dto.File, "financial-literacy-event-captions");
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
                        financialDevelopment.Translations.Add(new FinancialLiteracyEventCaptionTranslation
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

        public async Task<FinancialLiteracyEventCaptionGetDTO?> GetFirst()
        {
            FinancialLiteracyEventCaption? financialDevelopment = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync(h => h.Id == 1);

            return financialDevelopment == null ? null : _mapper.Map<FinancialLiteracyEventCaptionGetDTO>(financialDevelopment);
        }
    }
}
