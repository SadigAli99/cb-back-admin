
using AutoMapper;
using CB.Application.DTOs.EventImage;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class EventImageService : IEventImageService
    {
        private readonly IGenericRepository<EventMedia> _repository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public EventImageService(
            IGenericRepository<EventMedia> repository,
            IFileService fileService,
            IMapper mapper
        )
        {
            _fileService = fileService;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<EventImageGetDTO>> GetAllAsync(int? id)
        {
            var query = _repository.GetQuery()
                        .Where(x => x.MediaType == MediaType.IMAGE);

            if (id != default(int)) query = query.Where(x => x.EventId == id);

            var entities = await query
                        .Include(x => x.Event)
                        .ThenInclude(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .ToListAsync();

            List<EventImageGetDTO> data = _mapper.Map<List<EventImageGetDTO>>(entities);
            return data;
        }
        public async Task<EventImageGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            EventImageGetDTO? data = entity is null ? null : _mapper.Map<EventImageGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(EventImageCreateDTO dto)
        {

            foreach (var file in dto.ImageFiles)
            {
                var entity = new EventMedia
                {
                    EventId = dto.EventId,
                    MediaType = MediaType.IMAGE,
                    Url = await _fileService.UploadAsync(file, "events"),
                };
                await _repository.AddAsync(entity);
            }

            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null) return false;
            return await _repository.DeleteAsync(entity);
        }

    }
}
