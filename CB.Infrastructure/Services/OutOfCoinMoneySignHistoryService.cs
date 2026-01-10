
using AutoMapper;
using CB.Application.DTOs.OutOfCoinMoneySignHistory;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class OutOfCoinMoneySignHistoryService : IOutOfCoinMoneySignHistoryService
    {
        private readonly IGenericRepository<MoneySignHistory> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public OutOfCoinMoneySignHistoryService(
            IGenericRepository<MoneySignHistory> repository,
            IGenericRepository<Language> languageRepository,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<List<OutOfCoinMoneySignHistoryGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(x => x.MoneySign)
                        .ThenInclude(x => x.Translations)
                        .Where(x => x.MoneySign.Type == MoneySignType.COINOUT)
                        .ToListAsync();

            List<OutOfCoinMoneySignHistoryGetDTO> data = _mapper.Map<List<OutOfCoinMoneySignHistoryGetDTO>>(entities);
            foreach (OutOfCoinMoneySignHistoryGetDTO item in data)
            {
                var entity = entities.FirstOrDefault(x => x.Id == item.Id);
                if (entity is null) continue;
                item.MoneySignTitle = entity.MoneySign.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            }
            return data;
        }
        public async Task<OutOfCoinMoneySignHistoryGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(x => x.MoneySign)
                        .ThenInclude(x => x.Translations)
                        .Where(x => x.MoneySign.Type == MoneySignType.COINOUT)
                        .FirstOrDefaultAsync(b => b.Id == id);

            if (entity is null) return null;
            OutOfCoinMoneySignHistoryGetDTO? data = _mapper.Map<OutOfCoinMoneySignHistoryGetDTO>(entity);
            data.MoneySignTitle = entity.MoneySign.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;

            return data;
        }
        public async Task<bool> CreateAsync(OutOfCoinMoneySignHistoryCreateDTO dto)
        {

            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<MoneySignHistory>(dto);
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");


                return new MoneySignHistoryTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, OutOfCoinMoneySignHistoryEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");


                return new MoneySignHistoryTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
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
