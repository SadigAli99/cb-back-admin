
using AutoMapper;
using CB.Application.DTOs.Advertisement;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Extensions;
using CB.Shared.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly IGenericRepository<Advertisement> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public AdvertisementService(
            IGenericRepository<Advertisement> repository,
            IGenericRepository<Language> languageRepository,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<List<AdvertisementGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<AdvertisementGetDTO> data = _mapper.Map<List<AdvertisementGetDTO>>(entities);
            return data;
        }
        public async Task<AdvertisementGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            AdvertisementGetDTO? data = entity is null ? null : _mapper.Map<AdvertisementGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(AdvertisementCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<Advertisement>(dto);
            dto.Slugs = SlugHelper.GenerateSlugs(dto.Titles);
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");
                dto.MetaTitles.TryGetValue(t.Key, out var metaTitle);
                dto.MetaDescriptions.TryGetValue(t.Key, out var metaDescription);
                dto.MetaKeywords.TryGetValue(t.Key, out var metaKeyword);
                dto.ShortDescriptions.TryGetValue(t.Key, out var shortDescription);
                dto.Descriptions.TryGetValue(t.Key, out var description);
                dto.Slugs.TryGetValue(t.Key, out var slug);

                return new AdvertisementTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Slug = slug,
                    MetaTitle = metaTitle,
                    MetaDescription = metaDescription,
                    MetaKeyword = metaKeyword,
                    ShortDescription = shortDescription,
                    Description = description,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, AdvertisementEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            dto.Slugs = SlugHelper.GenerateSlugs(dto.Titles);

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.MetaTitles.TryGetValue(t.Key, out var metaTitle);
                dto.MetaDescriptions.TryGetValue(t.Key, out var metaDescription);
                dto.MetaKeywords.TryGetValue(t.Key, out var metaKeyword);
                dto.Slugs.TryGetValue(t.Key, out var slug);
                dto.ShortDescriptions.TryGetValue(t.Key, out var shortDescription);
                dto.Descriptions.TryGetValue(t.Key, out var description);

                return new AdvertisementTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Slug = slug,
                    MetaTitle = metaTitle,
                    MetaDescription = metaDescription,
                    MetaKeyword = metaKeyword,
                    ShortDescription = shortDescription,
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
