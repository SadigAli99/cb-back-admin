
using AutoMapper;
using CB.Application.DTOs.FinancialEvent;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class FinancialEventService : IFinancialEventService
    {
        private readonly IGenericRepository<FinancialEvent> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public FinancialEventService(
            IGenericRepository<FinancialEvent> repository,
            IGenericRepository<Language> languageRepository,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<List<FinancialEventGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<FinancialEventGetDTO> data = _mapper.Map<List<FinancialEventGetDTO>>(entities);
            return data;
        }
        public async Task<FinancialEventGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            FinancialEventGetDTO? data = entity is null ? null : _mapper.Map<FinancialEventGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(FinancialEventCreateDTO dto)
        {

            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<FinancialEvent>(dto);
            dto.Slugs = SlugHelper.GenerateSlugs(dto.Titles);
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Slugs.TryGetValue(t.Key, out var slug);
                dto.Descriptions.TryGetValue(t.Key, out var description);


                return new FinancialEventTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Slug = slug,
                    Description = description,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, FinancialEventEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            var languages = await _languageRepository.GetAllAsync();

            dto.Slugs = SlugHelper.GenerateSlugs(dto.Titles);

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Slugs.TryGetValue(t.Key, out var slug);
                dto.Descriptions.TryGetValue(t.Key, out var description);


                return new FinancialEventTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Slug = slug,
                    Description = description,
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
