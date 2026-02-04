using AutoMapper;
using CB.Application.DTOs.DigitalPortalCaption;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class DigitalPortalCaptionService : IDigitalPortalCaptionService
    {
        private readonly IGenericRepository<DigitalPortalCaption> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public DigitalPortalCaptionService(
            IMapper mapper,
            IGenericRepository<DigitalPortalCaption> repository,
            IGenericRepository<Language> languageRepository
        )
        {
            _mapper = mapper;
            _repository = repository;
            _languageRepository = languageRepository;
        }

        public async Task<bool> CreateOrUpdate(DigitalPortalCaptionPostDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            DigitalPortalCaption? entity = await _repository.GetQuery()
                                        .Include(h => h.Translations)
                                        .ThenInclude(ht => ht.Language)
                                        .FirstOrDefaultAsync();

            bool result;

            if (entity is null)
            {
                entity = _mapper.Map<DigitalPortalCaption>(dto);

                entity.Translations = dto.Titles.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    dto.Descriptions.TryGetValue(v.Key, out var description);

                    return new DigitalPortalCaptionTranslation
                    {
                        LanguageId = lang.Id,
                        Title = v.Value,
                        Description = description ?? string.Empty,
                    };
                }).ToList();

                result = await _repository.AddAsync(entity);
            }
            else
            {
                _mapper.Map(dto, entity);

                foreach (var v in dto.Titles)
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    dto.Descriptions.TryGetValue(v.Key, out var description);

                    var existingTranslation = entity.Translations.FirstOrDefault(t => t.LanguageId == lang.Id);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Title = v.Value;
                        existingTranslation.Description = description ?? string.Empty;
                    }
                    else
                    {
                        entity.Translations.Add(new DigitalPortalCaptionTranslation
                        {
                            LanguageId = lang.Id,
                            Title = v.Value,
                            Description = description ?? string.Empty,
                        });
                    }
                }

                result = await _repository.UpdateAsync(entity);
            }


            return result;
        }

        public async Task<DigitalPortalCaptionGetDTO?> GetFirst()
        {
            DigitalPortalCaption? entity = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync(h => h.Id == 1);

            return entity == null ? null : _mapper.Map<DigitalPortalCaptionGetDTO>(entity);
        }
    }
}
