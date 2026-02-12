
using AutoMapper;
using CB.Application.DTOs.Page;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class PageService : IPageService
    {
        private readonly IGenericRepository<Page> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public PageService(
            IGenericRepository<Language> languageRepository,
            IGenericRepository<Page> repository,
            IFileService fileService,
            IMapper mapper
        )
        {
            _languageRepository = languageRepository;
            _repository = repository;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<List<PageGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<PageGetDTO> data = _mapper.Map<List<PageGetDTO>>(entities);
            return data;
        }
        public async Task<PageGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            PageGetDTO? data = entity is null ? null : _mapper.Map<PageGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(PageCreateDTO dto)
        {

            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<Page>(dto);
            if (dto.File != null) entity.Image = await _fileService.UploadAsync(dto.File, "pages");
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");


                dto.Urls.TryGetValue(t.Key, out var url);
                dto.MetaTitles.TryGetValue(t.Key, out var metaTitle);
                dto.MetaDescriptions.TryGetValue(t.Key, out var metaDescription);

                return new PageTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Url = url,
                    MetaTitle = metaTitle,
                    MetaDescription = metaDescription,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, PageEditDTO dto)
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

                return new PageTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Url = url,
                    MetaTitle = metaTitle,
                    MetaDescription = metaDescription,
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
