
using AutoMapper;
using CB.Application.DTOs.InstantPaymentFAQ;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class InstantPaymentFAQService : IInstantPaymentFAQService
    {
        private readonly IGenericRepository<InstantPaymentFAQ> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public InstantPaymentFAQService(
            IGenericRepository<InstantPaymentFAQ> repository,
            IGenericRepository<Language> languageRepository,
            IMapper mapper
        )
        {
            _languageRepository = languageRepository;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<InstantPaymentFAQGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<InstantPaymentFAQGetDTO> data = _mapper.Map<List<InstantPaymentFAQGetDTO>>(entities);
            return data;
        }
        public async Task<InstantPaymentFAQGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            InstantPaymentFAQGetDTO? data = entity is null ? null : _mapper.Map<InstantPaymentFAQGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(InstantPaymentFAQCreateDTO dto)
        {

            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<InstantPaymentFAQ>(dto);
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Descriptions.TryGetValue(t.Key, out var description);

                return new InstantPaymentFAQTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Description = description ?? string.Empty,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, InstantPaymentFAQEditDTO dto)
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

                return new InstantPaymentFAQTranslation
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
