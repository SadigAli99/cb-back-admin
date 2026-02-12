
using AutoMapper;
using CB.Application.DTOs.DigitalPortal;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class DigitalPortalService : IDigitalPortalService
    {
        private readonly IGenericRepository<DigitalPortal> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public DigitalPortalService(
            IGenericRepository<DigitalPortal> repository,
            IGenericRepository<Language> languageRepository,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<List<DigitalPortalGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<DigitalPortalGetDTO> data = _mapper.Map<List<DigitalPortalGetDTO>>(entities);
            return data;
        }
        public async Task<DigitalPortalGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            DigitalPortalGetDTO? data = entity is null ? null : _mapper.Map<DigitalPortalGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(DigitalPortalCreateDTO dto)
        {

            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<DigitalPortal>(dto);
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Texts.TryGetValue(t.Key, out var text);

                return new DigitalPortalTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Text = text ?? string.Empty,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, DigitalPortalEditDTO dto)
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

                dto.Texts.TryGetValue(t.Key, out var text);

                return new DigitalPortalTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Text = text ?? string.Empty
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
