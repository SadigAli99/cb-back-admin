
using AutoMapper;
using CB.Application.DTOs.CurrencyHistoryPrevItemCharacteristic;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class CurrencyHistoryPrevItemCharacteristicService : ICurrencyHistoryPrevItemCharacteristicService
    {
        private readonly IGenericRepository<CurrencyHistoryPrevItemCharacteristic> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public CurrencyHistoryPrevItemCharacteristicService(
            IGenericRepository<CurrencyHistoryPrevItemCharacteristic> repository,
            IGenericRepository<Language> languageRepository,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<List<CurrencyHistoryPrevItemCharacteristicGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(x => x.CurrencyHistoryPrev)
                        .ThenInclude(x => x.Translations)
                        .ToListAsync();

            List<CurrencyHistoryPrevItemCharacteristicGetDTO> data = _mapper.Map<List<CurrencyHistoryPrevItemCharacteristicGetDTO>>(entities);

            foreach (var item in data)
            {
                var entity = entities.FirstOrDefault(x => x.Id == item.Id);
                if (entity is null) continue;
                item.CurrencyHistoryPrevItemTitle = entity.CurrencyHistoryPrev?.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            }
            return data;
        }
        public async Task<CurrencyHistoryPrevItemCharacteristicGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(x => x.CurrencyHistoryPrev)
                        .ThenInclude(x => x.Translations)
                        .FirstOrDefaultAsync(b => b.Id == id);

            if (entity is null) return null;
            CurrencyHistoryPrevItemCharacteristicGetDTO? data = _mapper.Map<CurrencyHistoryPrevItemCharacteristicGetDTO>(entity);
            data.CurrencyHistoryPrevItemTitle = entity.CurrencyHistoryPrev?.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            return data;
        }
        public async Task<bool> CreateAsync(CurrencyHistoryPrevItemCharacteristicCreateDTO dto)
        {

            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<CurrencyHistoryPrevItemCharacteristic>(dto);
            entity.Translations = dto.Labels.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Values.TryGetValue(t.Key, out var Value);

                return new CurrencyHistoryPrevItemCharacteristicTranslation
                {
                    LanguageId = lang.Id,
                    Label = t.Value,
                    Value = Value ?? string.Empty,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, CurrencyHistoryPrevItemCharacteristicEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Labels.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Values.TryGetValue(t.Key, out var Value);

                return new CurrencyHistoryPrevItemCharacteristicTranslation
                {
                    LanguageId = lang.Id,
                    Label = t.Value,
                    Value = Value ?? string.Empty
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
