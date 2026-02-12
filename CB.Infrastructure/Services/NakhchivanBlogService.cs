
using AutoMapper;
using CB.Application.DTOs.NakhchivanBlog;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class NakhchivanBlogService : INakhchivanBlogService
    {
        private readonly IGenericRepository<NakhchivanBlog> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public NakhchivanBlogService(
            IGenericRepository<NakhchivanBlog> repository,
            IGenericRepository<Language> languageRepository,
            IFileService fileService,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<List<NakhchivanBlogGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(b => b.Images)
                        .ToListAsync();

            List<NakhchivanBlogGetDTO> data = _mapper.Map<List<NakhchivanBlogGetDTO>>(entities);

            foreach (var entity in entities)
            {
                NakhchivanBlogGetDTO? dTO = data.FirstOrDefault(x => x.Id == entity.Id);
                if (dTO is null) continue;
                dTO.Images = _mapper.Map<List<NakhchivanBlogImageDTO>>(entity.Images);
            }
            return data;
        }
        public async Task<NakhchivanBlogGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(b => b.Images)
                        .FirstOrDefaultAsync(b => b.Id == id);

            NakhchivanBlogGetDTO? data = entity is null ? null : _mapper.Map<NakhchivanBlogGetDTO>(entity);

            if (data != null) data.Images = _mapper.Map<List<NakhchivanBlogImageDTO>>(entity?.Images);

            return data;
        }
        public async Task<bool> CreateAsync(NakhchivanBlogCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<NakhchivanBlog>(dto);
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Descriptions.TryGetValue(t.Key, out var description);

                return new NakhchivanBlogTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Description = description
                };
            }).ToList();

            foreach (IFormFile file in dto.Files)
            {
                entity.Images?.Add(new NakhchivanBlogImage
                {
                    Image = await _fileService.UploadAsync(file, "nakhchivan-blogs"),
                });
            }

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, NakhchivanBlogEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(b => b.Images)
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

                return new NakhchivanBlogTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Description = description,
                };
            }).ToList();

            foreach (IFormFile file in dto.Files)
            {
                entity.Images?.Add(new NakhchivanBlogImage
                {
                    Image = await _fileService.UploadAsync(file, "galleries"),
                });
            }



            return await _repository.UpdateAsync(entity);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null) return false;
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
