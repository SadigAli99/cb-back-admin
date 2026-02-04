using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CB.Application.DTOs.PercentCorridor;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class PercentCorridorService : IPercentCorridorService
    {
        private readonly IGenericRepository<PercentCorridor> _repository;
        private readonly IMapper _mapper;

        public PercentCorridorService(IGenericRepository<PercentCorridor> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<PercentCorridorGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                            .Include(x => x.PercentCorridorCategory!)
                            .ThenInclude(x => x.Translations)
                            .Where(x => x.DeletedAt == null)
                            .OrderByDescending(x => x.Date)
                            .ToListAsync();
            List<PercentCorridorGetDTO> data = _mapper.Map<List<PercentCorridorGetDTO>>(entities);
            foreach (PercentCorridorGetDTO item in data)
            {
                PercentCorridor? entity = entities.FirstOrDefault(x => x.Id == item.Id);
                item.PercentCategoryTitle = entity?.PercentCorridorCategory?.Translations.Where(x => x.LanguageId == 1).FirstOrDefault()?.Title;
            }
            return data;
        }

        public async Task<PercentCorridorGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null)
                return null;

            return _mapper.Map<PercentCorridorGetDTO>(entity);
        }

        public async Task<bool> CreateAsync(PercentCorridorCreateDTO dto)
        {
            var entity = _mapper.Map<PercentCorridor>(dto);
            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, PercentCorridorEditDTO dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null)
                return false;

            _mapper.Map(dto, entity);
            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null)
                return false;


            return await _repository.DeleteAsync(entity); ;
        }

        public async Task<PercentCorridorEditDTO?> GetForEditAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity is null ? null : _mapper.Map<PercentCorridorEditDTO>(entity);
        }
    }
}
