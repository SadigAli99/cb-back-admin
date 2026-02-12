
using AutoMapper;
using CB.Application.DTOs.Blog;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class BlogService : IBlogService
    {
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IGenericRepository<Blog> _repository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public BlogService(
            IGenericRepository<Language> languageRepository,
            IGenericRepository<Blog> repository,
            IFileService fileService,
            IMapper mapper
        )
        {
            _languageRepository = languageRepository;
            _fileService = fileService;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<BlogGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(b => b.Images)
                        .ToListAsync();

            List<BlogGetDTO> data = _mapper.Map<List<BlogGetDTO>>(entities);

            foreach (var entity in entities)
            {
                BlogGetDTO? dTO = data.FirstOrDefault(x => x.Id == entity.Id);
                if (dTO is null) continue;
                dTO.Images = _mapper.Map<List<BlogImageDTO>>(entity.Images);
            }
            return data;
        }
        public async Task<BlogGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(b => b.Images)
                        .FirstOrDefaultAsync(b => b.Id == id);

            BlogGetDTO? data = entity is null ? null : _mapper.Map<BlogGetDTO>(entity);

            if (data != null) data.Images = _mapper.Map<List<BlogImageDTO>>(entity?.Images);

            return data;
        }
        public async Task<bool> CreateAsync(BlogCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<Blog>(dto);
            entity.Image = await _fileService.UploadAsync(dto.ImageFile, "blogs");
            dto.Slugs = SlugHelper.GenerateSlugs(dto.Titles);
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.ImageAlts.TryGetValue(t.Key, out var imageAlt);
                dto.ImageTitles.TryGetValue(t.Key, out var imageTitle);
                dto.MetaTitles.TryGetValue(t.Key, out var metaTitle);
                dto.MetaDescriptions.TryGetValue(t.Key, out var metaDescription);
                dto.MetaKeywords.TryGetValue(t.Key, out var metaKeyword);
                dto.ShortDescriptions.TryGetValue(t.Key, out var shortDescription);
                dto.Descriptions.TryGetValue(t.Key, out var description);
                dto.Slugs.TryGetValue(t.Key, out var slug);

                return new BlogTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Slug = slug,
                    ImageAlt = imageAlt,
                    ImageTitle = imageTitle,
                    MetaTitle = metaTitle,
                    MetaDescription = metaDescription,
                    MetaKeyword = metaKeyword,
                    ShortDescription = shortDescription,
                    Description = description,
                };
            }).ToList();

            foreach (IFormFile file in dto.Files)
            {
                entity.Images?.Add(new BlogImage
                {
                    Image = await _fileService.UploadAsync(file, "blogs"),
                });
            }

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, BlogEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(b => b.Images)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            if (dto.ImageFile != null)
            {
                _fileService.Delete(entity.Image);
                entity.Image = await _fileService.UploadAsync(dto.ImageFile, "blogs");
            }

            dto.Slugs = SlugHelper.GenerateSlugs(dto.Titles);

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.ImageAlts.TryGetValue(t.Key, out var imageAlt);
                dto.ImageTitles.TryGetValue(t.Key, out var imageTitle);
                dto.MetaTitles.TryGetValue(t.Key, out var metaTitle);
                dto.MetaDescriptions.TryGetValue(t.Key, out var metaDescription);
                dto.MetaKeywords.TryGetValue(t.Key, out var metaKeyword);
                dto.Slugs.TryGetValue(t.Key, out var slug);
                dto.ShortDescriptions.TryGetValue(t.Key, out var shortDescription);
                dto.Descriptions.TryGetValue(t.Key, out var description);

                return new BlogTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Slug = slug,
                    ImageAlt = imageAlt,
                    ImageTitle = imageTitle,
                    MetaTitle = metaTitle,
                    MetaDescription = metaDescription,
                    MetaKeyword = metaKeyword,
                    ShortDescription = shortDescription,
                    Description = description,
                };
            }).ToList();

            foreach (IFormFile file in dto.Files)
            {
                entity.Images?.Add(new BlogImage
                {
                    Image = await _fileService.UploadAsync(file, "blogs"),
                });
            }



            return await _repository.UpdateAsync(entity);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null) return false;
            _fileService.Delete(entity.Image);
            return await _repository.DeleteAsync(entity);
        }

        public async Task<bool> DeleteImageAsync(int blogId, int imageId)
        {
            var entity = await _repository.GetQuery()
                            .Include(b => b.Images)
                            .FirstOrDefaultAsync(b => b.Id == blogId);

            if (entity is null) return false;


            var image = entity.Images.FirstOrDefault(i => i.Id == imageId);
            if (image is null) return false;
            _fileService.Delete(image.Image);
            entity.Images.Remove(image);

            return await _repository.UpdateAsync(entity);
        }


    }
}
