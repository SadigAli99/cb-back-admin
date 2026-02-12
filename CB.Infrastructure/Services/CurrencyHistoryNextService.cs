
using AutoMapper;
using CB.Application.DTOs.CurrencyHistoryNext;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CB.Infrastructure.Services
{
    public class CurrencyHistoryNextService : ICurrencyHistoryNextService
    {
        private readonly IGenericRepository<CurrencyHistoryNext> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public CurrencyHistoryNextService(
            IGenericRepository<CurrencyHistoryNext> repository,
            IGenericRepository<Language> languageRepository,
            IMapper mapper
        )
        {
            _languageRepository = languageRepository;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CurrencyHistoryNextGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(x => x.CurrencyHistory)
                        .ThenInclude(x => x.Translations)
                        .ToListAsync();

            List<CurrencyHistoryNextGetDTO> data = _mapper.Map<List<CurrencyHistoryNextGetDTO>>(entities);
            foreach (CurrencyHistoryNextGetDTO item in data)
            {
                var entity = entities.FirstOrDefault(x => x.Id == item.Id);
                if (entity is null) continue;
                item.CurrencyHistoryTitle = entity.CurrencyHistory.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            }
            return data;
        }
        public async Task<CurrencyHistoryNextGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(x => x.CurrencyHistory)
                        .ThenInclude(x => x.Translations)
                        .FirstOrDefaultAsync(b => b.Id == id);

            if (entity is null) return null;

            CurrencyHistoryNextGetDTO? data = _mapper.Map<CurrencyHistoryNextGetDTO>(entity);
            data.CurrencyHistoryTitle = entity.CurrencyHistory.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            return data;
        }
        public async Task<bool> CreateAsync(CurrencyHistoryNextCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<CurrencyHistoryNext>(dto);
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Descriptions.TryGetValue(t.Key, out var description);

                return new CurrencyHistoryNextTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Description = description
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, CurrencyHistoryNextEditDTO dto)
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

                return new CurrencyHistoryNextTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Description = description
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
