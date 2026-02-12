
using AutoMapper;
using CB.Application.DTOs.PageDetail;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class PageDetailService : IPageDetailService
    {
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IGenericRepository<PageDetail> _repository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public PageDetailService(
            IGenericRepository<Language> languageRepository,
            IGenericRepository<PageDetail> repository,
            IFileService fileService,
            IMapper mapper
        )
        {
            _languageRepository = languageRepository;
            _repository = repository;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<List<PageDetailGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(x => x.Page)
                        .ThenInclude(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .ToListAsync();

            List<PageDetailGetDTO> data = _mapper.Map<List<PageDetailGetDTO>>(entities);

            foreach (var item in data)
            {
                var entity = entities.FirstOrDefault(x => x.Id == item.Id);
                if (entity is null) continue;
                item.Page = entity.Page?.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            }
            return data;
        }
        public async Task<PageDetailGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(x => x.Page)
                        .ThenInclude(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            if (entity is null) return null;
            PageDetailGetDTO? data = _mapper.Map<PageDetailGetDTO>(entity);
            data.Page = entity.Page?.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            return data;
        }
        public async Task<bool> CreateAsync(PageDetailCreateDTO dto)
        {

            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<PageDetail>(dto);

            if (dto.File != null) entity.Image = await _fileService.UploadAsync(dto.File, "pages");

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Urls.TryGetValue(t.Key, out var url);
                dto.MetaTitles.TryGetValue(t.Key, out var metaTitle);
                dto.MetaDescriptions.TryGetValue(t.Key, out var metaDescription);

                return new PageDetailTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Url = url ?? string.Empty,
                    MetaTitle = metaTitle ?? string.Empty,
                    MetaDescription = metaDescription ?? string.Empty,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, PageDetailEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            if (dto.File != null)
            {
                _fileService.Delete( entity.Image ?? "");
                entity.Image = await _fileService.UploadAsync(dto.File, "pages");
            }

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Urls.TryGetValue(t.Key, out var url);
                dto.MetaTitles.TryGetValue(t.Key, out var metaTitle);
                dto.MetaDescriptions.TryGetValue(t.Key, out var metaDescription);

                return new PageDetailTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Url = url ?? string.Empty,
                    MetaTitle = metaTitle ?? string.Empty,
                    MetaDescription = metaDescription ?? string.Empty,
                };
            }).ToList();

            return await _repository.UpdateAsync(entity);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null) return false;
            _fileService.Delete( entity.Image ?? "");

            return await _repository.DeleteAsync(entity);
        }

    }
}
