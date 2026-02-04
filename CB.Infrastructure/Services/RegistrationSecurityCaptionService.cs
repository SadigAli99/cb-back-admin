using AutoMapper;
using CB.Application.DTOs.RegistrationSecurityCaption;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class RegistrationSecurityCaptionService : IRegistrationSecurityCaptionService
    {
        private readonly IGenericRepository<RegistrationSecurityCaption> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public RegistrationSecurityCaptionService(
            IMapper mapper,
            IGenericRepository<RegistrationSecurityCaption> repository,
            IGenericRepository<Language> languageRepository
        )
        {
            _mapper = mapper;
            _repository = repository;
            _languageRepository = languageRepository;
        }

        public async Task<bool> CreateOrUpdate(RegistrationSecurityCaptionPostDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            RegistrationSecurityCaption? registrationSecurityCaption = await _repository.GetQuery()
                                        .Include(h => h.Translations)
                                        .ThenInclude(ht => ht.Language)
                                        .FirstOrDefaultAsync();

            bool result;

            if (registrationSecurityCaption is null)
            {
                registrationSecurityCaption = _mapper.Map<RegistrationSecurityCaption>(dto);

                registrationSecurityCaption.Translations = dto.Descriptions.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    return new RegistrationSecurityCaptionTranslation
                    {
                        LanguageId = lang.Id,
                        Description = v.Value,
                    };
                }).ToList();

                result = await _repository.AddAsync(registrationSecurityCaption);
            }
            else
            {
                _mapper.Map(dto, registrationSecurityCaption);

                foreach (var v in dto.Descriptions)
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    var existingTranslation = registrationSecurityCaption.Translations.FirstOrDefault(t => t.LanguageId == lang.Id);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Description = v.Value;
                    }
                    else
                    {
                        registrationSecurityCaption.Translations.Add(new RegistrationSecurityCaptionTranslation
                        {
                            LanguageId = lang.Id,
                            Description = v.Value
                        });
                    }
                }

                result = await _repository.UpdateAsync(registrationSecurityCaption);
            }


            return result;
        }

        public async Task<RegistrationSecurityCaptionGetDTO?> GetFirst()
        {
            RegistrationSecurityCaption? registrationSecurityCaption = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync(h => h.Id == 1);

            return registrationSecurityCaption == null ? null : _mapper.Map<RegistrationSecurityCaptionGetDTO>(registrationSecurityCaption);
        }
    }
}
