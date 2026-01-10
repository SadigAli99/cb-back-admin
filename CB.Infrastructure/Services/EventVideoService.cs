
using AutoMapper;
using CB.Application.DTOs.EventVideo;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class EventVideoService : IEventVideoService
    {
        private readonly IGenericRepository<EventMedia> _repository;
        private readonly IMapper _mapper;

        public EventVideoService(
            IGenericRepository<EventMedia> repository,
            IMapper mapper
        )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<EventVideoGetDTO>> GetAllAsync(int? id)
        {
            var query = _repository.GetQuery()
                        .Where(x => x.MediaType == MediaType.VIDEO);

            if (id != default(int)) query = query.Where(x => x.EventId == id);

            var entities = await query
                        .Include(x => x.Event)
                        .ThenInclude(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .ToListAsync();

            List<EventVideoGetDTO> data = _mapper.Map<List<EventVideoGetDTO>>(entities);
            return data;
        }
        public async Task<EventVideoGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            EventVideoGetDTO? data = entity is null ? null : _mapper.Map<EventVideoGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(EventVideoCreateDTO dto)
        {
            var entity = _mapper.Map<EventMedia>(dto);
            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, EventVideoEditDTO dto)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

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
