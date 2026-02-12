
using AutoMapper;
using CB.Application.DTOs.EventContent;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class EventContentService : IEventContentService
    {
        private readonly IGenericRepository<EventContent> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public EventContentService(
            IGenericRepository<EventContent> repository,
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

        public async Task<List<EventContentGetDTO>> GetAllAsync(int? id)
        {
            var query = _repository.GetQuery();

            if (id != default(int)) query = query.Where(x => x.EventId == id);

            var entities = await query
                        .Include(x => x.Event)
                        .ThenInclude(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(b => b.Images)
                        .ToListAsync();


            List<EventContentGetDTO> data = _mapper.Map<List<EventContentGetDTO>>(entities);

            foreach (var entity in entities)
            {
                EventContentGetDTO? dTO = data.FirstOrDefault(x => x.Id == entity.Id);
                if (dTO is null) continue;
                dTO.Images = _mapper.Map<List<EventContentImageDTO>>(entity.Images);
                dTO.EventTitle = entity.Event.Translations.FirstOrDefault()?.Title;
            }
            return data;
        }
        public async Task<EventContentGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(b => b.Images)
                        .FirstOrDefaultAsync(b => b.Id == id);

            EventContentGetDTO? data = entity is null ? null : _mapper.Map<EventContentGetDTO>(entity);

            if (data != null)
            {
                data.Images = _mapper.Map<List<EventContentImageDTO>>(entity?.Images);
                data.EventTitle = entity?.Event.Translations.FirstOrDefault()?.Title;
            }

            return data;
        }
        public async Task<bool> CreateAsync(EventContentCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<EventContent>(dto);
            dto.Slugs = SlugHelper.GenerateSlugs(dto.Titles);
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");
                dto.Descriptions.TryGetValue(t.Key, out var description);
                dto.Slugs.TryGetValue(t.Key, out var slug);

                return new EventContentTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Slug = slug,
                    Description = description,
                };
            }).ToList();

            foreach (IFormFile file in dto.ImageFiles)
            {
                entity.Images?.Add(new EventContentImage
                {
                    Image = await _fileService.UploadAsync(file, "event-contents"),
                });
            }

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, EventContentEditDTO dto)
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

                return new EventContentTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Slug = slug,
                    Description = description,
                };
            }).ToList();

            foreach (IFormFile file in dto.ImageFiles)
            {
                entity.Images?.Add(new EventContentImage
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
