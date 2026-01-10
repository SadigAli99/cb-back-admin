
using AutoMapper;
using CB.Application.DTOs.TerritorialOfficeRegion;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class TerritorialOfficeRegionService : ITerritorialOfficeRegionService
    {
        private readonly IGenericRepository<TerritorialOfficeRegion> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public TerritorialOfficeRegionService(
            IGenericRepository<TerritorialOfficeRegion> repository,
            IGenericRepository<Language> languageRepository,
            IWebHostEnvironment env,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
            _env = env;
        }

        public async Task<List<TerritorialOfficeRegionGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(x => x.TerritorialOffice)
                        .ThenInclude(x => x.Translations)
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<TerritorialOfficeRegionGetDTO> data = _mapper.Map<List<TerritorialOfficeRegionGetDTO>>(entities);

            foreach (var item in data)
            {
                var entity = entities.FirstOrDefault(x => x.Id == item.Id);
                item.TerritorialOfficeTitle = entity?.TerritorialOffice?.Translations.FirstOrDefault()?.Title;
            }
            return data;
        }
        public async Task<TerritorialOfficeRegionGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(x => x.TerritorialOffice)
                        .ThenInclude(x => x.Translations)
                        .FirstOrDefaultAsync(b => b.Id == id);
            if (entity is null) return null;

            TerritorialOfficeRegionGetDTO? data = _mapper.Map<TerritorialOfficeRegionGetDTO>(entity);
            data.TerritorialOfficeTitle = entity?.TerritorialOffice.Translations.FirstOrDefault()?.Title;
            return data;
        }
        public async Task<bool> CreateAsync(TerritorialOfficeRegionCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<TerritorialOfficeRegion>(dto);
            entity.Image = await dto.File.FileUpload(_env.WebRootPath, "territorial-offices");
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Directors.TryGetValue(t.Key, out var director);
                dto.Locations.TryGetValue(t.Key, out var location);


                return new TerritorialOfficeRegionTranslation
                {
                    LanguageId = lang.Id,
                    Director = director,
                    Location = location,
                    Title = t.Value,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, TerritorialOfficeRegionEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            if (dto.File != null)
            {
                FileManager.FileDelete(_env.WebRootPath, entity.Image ?? "");
                entity.Image = await dto.File.FileUpload(_env.WebRootPath, "territorial-offices");
            }

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);

                dto.Directors.TryGetValue(t.Key, out var director);
                dto.Locations.TryGetValue(t.Key, out var location);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                return new TerritorialOfficeRegionTranslation
                {
                    LanguageId = lang.Id,
                    Director = director,
                    Location = location,
                    Title = t.Value,
                };
            }).ToList();



            return await _repository.UpdateAsync(entity);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null) return false;
            FileManager.FileDelete(_env.WebRootPath, entity.Image ?? "");
            return await _repository.DeleteAsync(entity);
        }

    }
}
