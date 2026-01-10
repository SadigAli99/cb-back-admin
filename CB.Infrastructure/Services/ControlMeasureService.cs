
using AutoMapper;
using CB.Application.DTOs.ControlMeasure;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class ControlMeasureService : IControlMeasureService
    {
        private readonly IGenericRepository<ControlMeasure> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public ControlMeasureService(
            IGenericRepository<ControlMeasure> repository,
            IGenericRepository<Language> languageRepository,
             IMapper mapper
             )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<List<ControlMeasureGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                            .Include(x => x.ControlMeasureCategory)
                            .ThenInclude(x => x.Translations)
                            .ThenInclude(x => x.Language)
                            .Include(x => x.Translations)
                            .ThenInclude(x => x.Language)
                            .OrderByDescending(x => x.Year)
                            .ToListAsync();
            List<ControlMeasureGetDTO> data = _mapper.Map<List<ControlMeasureGetDTO>>(entities);
            foreach (ControlMeasureGetDTO item in data)
            {
                ControlMeasure? entity = entities.FirstOrDefault(x => x.Id == item.Id);
                item.ControlMeasureCategoryTitle = entity?.ControlMeasureCategory?.Translations.FirstOrDefault()?.Title;
            }
            return data;
        }

        public async Task<ControlMeasureGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                            .Include(x => x.ControlMeasureCategory)
                            .ThenInclude(x => x.Translations)
                            .ThenInclude(x => x.Language)
                            .Include(x => x.Translations)
                            .ThenInclude(x => x.Language)
                            .FirstOrDefaultAsync(x => x.Id == id);
            if (entity is null)
                return null;

            return _mapper.Map<ControlMeasureGetDTO>(entity);
        }

        public async Task<bool> CreateAsync(ControlMeasureCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<ControlMeasure>(dto);
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Descriptions.TryGetValue(t.Key, out var description);

                return new ControlMeasureTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Description = description,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, ControlMeasureEditDTO dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null)
                return false;

            _mapper.Map(dto, entity);
            var languages = await _languageRepository.GetAllAsync();


            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Descriptions.TryGetValue(t.Key, out var description);

                return new ControlMeasureTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Description = description,
                };
            }).ToList();
            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null)
                return false;


            return await _repository.DeleteAsync(entity); ;
        }

        public async Task<ControlMeasureEditDTO?> GetForEditAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity is null ? null : _mapper.Map<ControlMeasureEditDTO>(entity);
        }
    }
}
