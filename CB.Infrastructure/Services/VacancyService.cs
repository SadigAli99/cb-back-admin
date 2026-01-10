
using AutoMapper;
using CB.Application.DTOs.Vacancy;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class VacancyService : IVacancyService
    {
        private readonly IGenericRepository<Vacancy> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public VacancyService(
            IGenericRepository<Vacancy> repository,
            IGenericRepository<Language> languageRepository,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<List<VacancyGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(x => x.Department)
                        .ThenInclude(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .Include(x => x.Branch)
                        .ThenInclude(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .Include(x => x.Position)
                        .ThenInclude(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<VacancyGetDTO> data = _mapper.Map<List<VacancyGetDTO>>(entities);
            return data;
        }
        public async Task<VacancyGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(x => x.Department)
                        .ThenInclude(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .Include(x => x.Branch)
                        .ThenInclude(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .Include(x => x.Position)
                        .ThenInclude(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            VacancyGetDTO? data = entity is null ? null : _mapper.Map<VacancyGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(VacancyCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            dto.Slugs = SlugHelper.GenerateSlugs(dto.Titles);
            var entity = _mapper.Map<Vacancy>(dto);
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Locations.TryGetValue(t.Key, out var location);
                dto.Slugs.TryGetValue(t.Key, out var slug);

                return new VacancyTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Slug = slug,
                    Location = location ?? string.Empty,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, VacancyEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            dto.Slugs = SlugHelper.GenerateSlugs(dto.Titles);

            _mapper.Map(dto, entity);

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Locations.TryGetValue(t.Key, out var location);
                dto.Slugs.TryGetValue(t.Key, out var slug);

                return new VacancyTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Slug = slug,
                    Location = location ?? string.Empty,
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
