
using AutoMapper;
using CB.Application.DTOs.InfographicDisclosure;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class InfographicDisclosureService : IInfographicDisclosureService
    {
        private readonly IGenericRepository<InfographicDisclosure> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public InfographicDisclosureService(
            IGenericRepository<InfographicDisclosure> repository,
            IGenericRepository<Language> languageRepository,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<List<InfographicDisclosureGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(x => x.InfographicDisclosureCategory!)
                        .ThenInclude(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .ToListAsync();

            List<InfographicDisclosureGetDTO> data = _mapper.Map<List<InfographicDisclosureGetDTO>>(entities);

            foreach (var item in data)
            {
                var entity = entities.FirstOrDefault(x => x.Id == item.Id);
                if (entity is null) continue;
                item.InfographicDisclosureCategory = entity.InfographicDisclosureCategory?.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
                item.InfographicDisclosureFrequency = entity.InfographicDisclosureFrequency?.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            }
            return data;
        }
        public async Task<InfographicDisclosureGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(x => x.InfographicDisclosureCategory!)
                        .ThenInclude(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            if (entity is null) return null;
            InfographicDisclosureGetDTO? data = _mapper.Map<InfographicDisclosureGetDTO>(entity);
            data.InfographicDisclosureCategory = entity.InfographicDisclosureCategory?.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            data.InfographicDisclosureFrequency = entity.InfographicDisclosureFrequency?.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;

            return data;
        }
        public async Task<bool> CreateAsync(InfographicDisclosureCreateDTO dto)
        {

            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<InfographicDisclosure>(dto);
            entity.Translations = dto.Deadlines.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Descriptions.TryGetValue(t.Key, out var Description);

                return new InfographicDisclosureTranslation
                {
                    LanguageId = lang.Id,
                    Deadline = t.Value,
                    Description = Description ?? string.Empty,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, InfographicDisclosureEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Deadlines.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Descriptions.TryGetValue(t.Key, out var Description);

                return new InfographicDisclosureTranslation
                {
                    LanguageId = lang.Id,
                    Deadline = t.Value,
                    Description = Description ?? string.Empty
                };
            }).ToList();

            return await _repository.UpdateAsync(entity);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null) return false;
            return await _repository.DeleteAsync(entity);
        }

    }
}
