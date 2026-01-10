
using AutoMapper;
using CB.Application.DTOs.OutOfBankNoteMoneySignCharacteristic;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class OutOfBankNoteMoneySignCharacteristicService : IOutOfBankNoteMoneySignCharacteristicService
    {
        private readonly IGenericRepository<MoneySignCharacteristic> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public OutOfBankNoteMoneySignCharacteristicService(
            IGenericRepository<MoneySignCharacteristic> repository,
            IGenericRepository<Language> languageRepository,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<List<OutOfBankNoteMoneySignCharacteristicGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(x => x.MoneySignHistory)
                        .ThenInclude(x => x.Translations)
                        .Include(x => x.MoneySignHistory)
                        .ThenInclude(x => x.MoneySign)
                        .Where(x => x.MoneySignHistory.MoneySign.Type == MoneySignType.BANKNOTEOUT)
                        .ToListAsync();

            List<OutOfBankNoteMoneySignCharacteristicGetDTO> data = _mapper.Map<List<OutOfBankNoteMoneySignCharacteristicGetDTO>>(entities);

            foreach (var item in data)
            {
                var entity = entities.FirstOrDefault(x => x.Id == item.Id);
                if (entity is null) continue;
                item.MoneySignHistoryTitle = entity.MoneySignHistory?.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            }
            return data;
        }
        public async Task<OutOfBankNoteMoneySignCharacteristicGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(x => x.MoneySignHistory)
                        .ThenInclude(x => x.Translations)
                        .Include(x => x.MoneySignHistory)
                        .ThenInclude(x => x.MoneySign)
                        .Where(x => x.MoneySignHistory.MoneySign.Type == MoneySignType.BANKNOTEOUT)
                        .FirstOrDefaultAsync(b => b.Id == id);

            if (entity is null) return null;
            OutOfBankNoteMoneySignCharacteristicGetDTO? data = _mapper.Map<OutOfBankNoteMoneySignCharacteristicGetDTO>(entity);
            data.MoneySignHistoryTitle = entity.MoneySignHistory?.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            return data;
        }
        public async Task<bool> CreateAsync(OutOfBankNoteMoneySignCharacteristicCreateDTO dto)
        {

            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<MoneySignCharacteristic>(dto);
            entity.Translations = dto.Labels.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Values.TryGetValue(t.Key, out var Value);

                return new MoneySignCharacteristicTranslation
                {
                    LanguageId = lang.Id,
                    Label = t.Value,
                    Value = Value ?? string.Empty,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, OutOfBankNoteMoneySignCharacteristicEditDTO dto)
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

                return new MoneySignCharacteristicTranslation
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
