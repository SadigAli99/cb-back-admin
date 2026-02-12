using AutoMapper;
using CB.Application.DTOs.FinancialSearchSystemCaption;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class FinancialSearchSystemCaptionService : IFinancialSearchSystemCaptionService
    {
        private readonly IGenericRepository<FinancialSearchSystemCaption> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public FinancialSearchSystemCaptionService(
            IGenericRepository<FinancialSearchSystemCaption> repository,
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

        public async Task<bool> CreateOrUpdate(FinancialSearchSystemCaptionPostDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            FinancialSearchSystemCaption? finalcialSearchSystemCaption = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync();

            bool result;

            if (finalcialSearchSystemCaption is null)
            {
                finalcialSearchSystemCaption = _mapper.Map<FinancialSearchSystemCaption>(dto);
                if (dto.File != null) finalcialSearchSystemCaption.Image = await _fileService.UploadAsync(dto.File, "financial-search-system-captions");


                finalcialSearchSystemCaption.Translations = dto.Titles.Select(v =>
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

                result = await _repository.AddAsync(finalcialSearchSystemCaption);
            }
            else
            {
                _mapper.Map(dto, finalcialSearchSystemCaption);

                if (dto.File != null)
                {
                    _fileService.Delete(finalcialSearchSystemCaption.Image ?? "");
                    finalcialSearchSystemCaption.Image = await _fileService.UploadAsync(dto.File, "financial-search-system-captions");
                }

                foreach (var v in dto.Titles)
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");


                    var existingTranslation = finalcialSearchSystemCaption.Translations.FirstOrDefault(t => t.LanguageId == lang.Id);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Title = v.Value;
                    }
                    else
                    {
                        finalcialSearchSystemCaption.Translations.Add(new FinancialSearchSystemCaptionTranslation
                        {
                            LanguageId = lang.Id,
                            Title = v.Value,
                        });
                    }
                }

                result = await _repository.UpdateAsync(finalcialSearchSystemCaption);
            }


            return result;
        }

        public async Task<FinancialSearchSystemCaptionGetDTO?> GetFirst()
        {
            FinancialSearchSystemCaption? finalcialSearchSystemCaption = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync(h => h.Id == 1);

            return finalcialSearchSystemCaption == null ? null : _mapper.Map<FinancialSearchSystemCaptionGetDTO>(finalcialSearchSystemCaption);
        }
    }
}
