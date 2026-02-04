using AutoMapper;
using CB.Application.DTOs.ElectronicMoneyInstitutionCaption;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class ElectronicMoneyInstitutionCaptionService : IElectronicMoneyInstitutionCaptionService
    {
        private readonly IGenericRepository<ElectronicMoneyInstitutionCaption> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public ElectronicMoneyInstitutionCaptionService(
            IMapper mapper,
            IGenericRepository<ElectronicMoneyInstitutionCaption> repository,
            IGenericRepository<Language> languageRepository
        )
        {
            _mapper = mapper;
            _repository = repository;
            _languageRepository = languageRepository;
        }

        public async Task<bool> CreateOrUpdate(ElectronicMoneyInstitutionCaptionPostDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            ElectronicMoneyInstitutionCaption? electronicMoneyInstitutionCaption = await _repository.GetQuery()
                                        .Include(h => h.Translations)
                                        .ThenInclude(ht => ht.Language)
                                        .FirstOrDefaultAsync();

            bool result;

            if (electronicMoneyInstitutionCaption is null)
            {
                electronicMoneyInstitutionCaption = _mapper.Map<ElectronicMoneyInstitutionCaption>(dto);

                electronicMoneyInstitutionCaption.Translations = dto.Descriptions.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    return new ElectronicMoneyInstitutionCaptionTranslation
                    {
                        LanguageId = lang.Id,
                        Description = v.Value,
                    };
                }).ToList();

                result = await _repository.AddAsync(electronicMoneyInstitutionCaption);
            }
            else
            {
                _mapper.Map(dto, electronicMoneyInstitutionCaption);

                foreach (var v in dto.Descriptions)
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    var existingTranslation = electronicMoneyInstitutionCaption.Translations.FirstOrDefault(t => t.LanguageId == lang.Id);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Description = v.Value;
                    }
                    else
                    {
                        electronicMoneyInstitutionCaption.Translations.Add(new ElectronicMoneyInstitutionCaptionTranslation
                        {
                            LanguageId = lang.Id,
                            Description = v.Value
                        });
                    }
                }

                result = await _repository.UpdateAsync(electronicMoneyInstitutionCaption);
            }


            return result;
        }

        public async Task<ElectronicMoneyInstitutionCaptionGetDTO?> GetFirst()
        {
            ElectronicMoneyInstitutionCaption? electronicMoneyInstitutionCaption = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync(h => h.Id == 1);

            return electronicMoneyInstitutionCaption == null ? null : _mapper.Map<ElectronicMoneyInstitutionCaptionGetDTO>(electronicMoneyInstitutionCaption);
        }
    }
}
