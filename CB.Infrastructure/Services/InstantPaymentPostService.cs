
using AutoMapper;
using CB.Application.DTOs.InstantPaymentPost;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Extensions;
using CB.Shared.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class InstantPaymentPostService : IInstantPaymentPostService
    {
        private readonly IGenericRepository<InstantPaymentPost> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public InstantPaymentPostService(
            IGenericRepository<InstantPaymentPost> repository,
            IGenericRepository<Language> languageRepository,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<List<InstantPaymentPostGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<InstantPaymentPostGetDTO> data = _mapper.Map<List<InstantPaymentPostGetDTO>>(entities);
            return data;
        }
        public async Task<InstantPaymentPostGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            InstantPaymentPostGetDTO? data = entity is null ? null : _mapper.Map<InstantPaymentPostGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(InstantPaymentPostCreateDTO dto)
        {

            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<InstantPaymentPost>(dto);
            dto.Slugs = SlugHelper.GenerateSlugs(dto.Titles);
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Slugs.TryGetValue(t.Key, out var slug);
                dto.ShortDescriptions.TryGetValue(t.Key, out var shortDescription);
                dto.Descriptions.TryGetValue(t.Key, out var description);

                return new InstantPaymentPostTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Slug = slug ?? string.Empty,
                    ShortDescription = shortDescription ?? string.Empty,
                    Description = description ?? string.Empty,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, InstantPaymentPostEditDTO dto)
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
                dto.Descrtiptions.TryGetValue(t.Key, out var description);
                dto.ShortDescriptions.TryGetValue(t.Key, out var ShortDescription);

                return new InstantPaymentPostTranslation
                {
                    LanguageId = lang.Id,
                    Slug = slug,
                    Title = t.Value,
                    ShortDescription = ShortDescription ?? string.Empty,
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
