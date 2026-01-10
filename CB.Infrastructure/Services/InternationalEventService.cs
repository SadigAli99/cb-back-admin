
using AutoMapper;
using CB.Application.DTOs.InternationalEvent;
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
    public class InternationalEventService : IInternationalEventService
    {
        private readonly IGenericRepository<InternationalEvent> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public InternationalEventService(
            IGenericRepository<InternationalEvent> repository,
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

        public async Task<List<InternationalEventGetDTO>> GetAllAsync()
        {
            var query = _repository.GetQuery();

            var entities = await query
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(b => b.Images)
                        .ToListAsync();


            List<InternationalEventGetDTO> data = _mapper.Map<List<InternationalEventGetDTO>>(entities);

            foreach (var entity in entities)
            {
                InternationalEventGetDTO? dTO = data.FirstOrDefault(x => x.Id == entity.Id);
                if (dTO is null) continue;
                dTO.Images = _mapper.Map<List<InternationalEventImageDTO>>(entity.Images);
            }
            return data;
        }
        public async Task<InternationalEventGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(b => b.Images)
                        .FirstOrDefaultAsync(b => b.Id == id);

            InternationalEventGetDTO? data = entity is null ? null : _mapper.Map<InternationalEventGetDTO>(entity);

            if (data != null)
            {
                data.Images = _mapper.Map<List<InternationalEventImageDTO>>(entity?.Images);
            }

            return data;
        }
        public async Task<bool> CreateAsync(InternationalEventCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<InternationalEvent>(dto);
            dto.Slugs = SlugHelper.GenerateSlugs(dto.Titles);
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");
                dto.Descriptions.TryGetValue(t.Key, out var description);
                dto.Slugs.TryGetValue(t.Key, out var slug);

                return new InternationalEventTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Slug = slug,
                    Description = description,
                };
            }).ToList();

            foreach (IFormFile file in dto.ImageFiles)
            {
                entity.Images?.Add(new InternationalEventImage
                {
                    Image = await file.FileUpload(_env.WebRootPath, "international-events"),
                });
            }

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, InternationalEventEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(b => b.Images)
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

                dto.Slugs.TryGetValue(t.Key, out var slug);
                dto.Descriptions.TryGetValue(t.Key, out var description);

                return new InternationalEventTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Slug = slug,
                    Description = description,
                };
            }).ToList();

            foreach (IFormFile file in dto.ImageFiles)
            {
                entity.Images?.Add(new InternationalEventImage
                {
                    Image = await file.FileUpload(_env.WebRootPath, "blogs"),
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
            FileManager.FileDelete(_env.WebRootPath, image.Image);
            entity.Images.Remove(image);

            return await _repository.UpdateAsync(entity);
        }


    }
}
