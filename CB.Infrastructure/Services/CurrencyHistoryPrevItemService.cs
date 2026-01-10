
using AutoMapper;
using CB.Application.DTOs.CurrencyHistoryPrevItem;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class CurrencyHistoryPrevItemService : ICurrencyHistoryPrevItemService
    {
        private readonly IGenericRepository<CurrencyHistoryPrevItem> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public CurrencyHistoryPrevItemService(
            IGenericRepository<CurrencyHistoryPrevItem> repository,
            IGenericRepository<Language> languageRepository,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<List<CurrencyHistoryPrevItemGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(x => x.CurrencyHistoryPrev)
                        .ThenInclude(x => x.Translations)
                        .ToListAsync();

            List<CurrencyHistoryPrevItemGetDTO> data = _mapper.Map<List<CurrencyHistoryPrevItemGetDTO>>(entities);
            foreach (CurrencyHistoryPrevItemGetDTO item in data)
            {
                var entity = entities.FirstOrDefault(x => x.Id == item.Id);
                if (entity is null) continue;
                item.CurrencyHistoryPrevTitle = entity.CurrencyHistoryPrev.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            }
            return data;
        }
        public async Task<CurrencyHistoryPrevItemGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(x => x.CurrencyHistoryPrev)
                        .ThenInclude(x => x.Translations)
                        .FirstOrDefaultAsync(b => b.Id == id);

            if (entity is null) return null;
            CurrencyHistoryPrevItemGetDTO? data = _mapper.Map<CurrencyHistoryPrevItemGetDTO>(entity);
            data.CurrencyHistoryPrevTitle = entity.CurrencyHistoryPrev.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;

            return data;
        }
        public async Task<bool> CreateAsync(CurrencyHistoryPrevItemCreateDTO dto)
        {
            var entity = _mapper.Map<CurrencyHistoryPrevItem>(dto);

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Descriptions.TryGetValue(t.Key, out var description);

                return new CurrencyHistoryPrevItemTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Description = description ?? string.Empty
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, CurrencyHistoryPrevItemEditDTO dto)
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

                dto.Descriptions.TryGetValue(t.Key, out var description);

                return new CurrencyHistoryPrevItemTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Description = description ?? string.Empty
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
