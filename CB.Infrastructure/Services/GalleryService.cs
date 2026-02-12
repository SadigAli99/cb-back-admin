
using AutoMapper;
using CB.Application.DTOs.Gallery;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class GalleryService : IGalleryService
    {
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IGenericRepository<Gallery> _repository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public GalleryService(
            IGenericRepository<Language> languageRepository,
            IGenericRepository<Gallery> repository,
            IFileService fileService,
            IMapper mapper
        )
        {
            _languageRepository = languageRepository;
            _fileService = fileService;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GalleryGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(b => b.Images)
                        .ToListAsync();

            List<GalleryGetDTO> data = _mapper.Map<List<GalleryGetDTO>>(entities);

            foreach (var entity in entities)
            {
                GalleryGetDTO? dTO = data.FirstOrDefault(x => x.Id == entity.Id);
                if (dTO is null) continue;
                dTO.Images = _mapper.Map<List<GalleryImageDTO>>(entity.Images);
            }
            return data;
        }
        public async Task<GalleryGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(b => b.Images)
                        .FirstOrDefaultAsync(b => b.Id == id);

            GalleryGetDTO? data = entity is null ? null : _mapper.Map<GalleryGetDTO>(entity);

            if (data != null) data.Images = _mapper.Map<List<GalleryImageDTO>>(entity?.Images);

            return data;
        }
        public async Task<bool> CreateAsync(GalleryCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<Gallery>(dto);
            entity.Image = await _fileService.UploadAsync(dto.ImageFile, "galleries");
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.ImageAlts.TryGetValue(t.Key, out var imageAlt);
                dto.ImageTitles.TryGetValue(t.Key, out var imageTitle);

                return new GalleryTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    ImageAlt = imageAlt,
                    ImageTitle = imageTitle,
                };
            }).ToList();

            foreach (IFormFile file in dto.Files)
            {
                entity.Images?.Add(new GalleryImage
                {
                    Image = await _fileService.UploadAsync(file, "galleries"),
                });
            }

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, GalleryEditDTO dto)
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
                entity.Image = await _fileService.UploadAsync(dto.ImageFile, "galleries");
            }

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.ImageAlts.TryGetValue(t.Key, out var imageAlt);
                dto.ImageTitles.TryGetValue(t.Key, out var imageTitle);

                return new GalleryTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    ImageAlt = imageAlt,
                    ImageTitle = imageTitle,
                };
            }).ToList();

            foreach (IFormFile file in dto.Files)
            {
                entity.Images?.Add(new GalleryImage
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
