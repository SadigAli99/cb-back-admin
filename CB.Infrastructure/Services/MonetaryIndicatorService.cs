
using AutoMapper;
using CB.Application.DTOs.MonetaryIndicator;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class MonetaryIndicatorService : IMonetaryIndicatorService
    {
        private readonly IGenericRepository<MonetaryIndicator> _repository;
        private readonly IMapper _mapper;

        public MonetaryIndicatorService(IGenericRepository<MonetaryIndicator> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<MonetaryIndicatorGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                            .Include(x => x.MonetaryIndicatorCategory!)
                            .ThenInclude(x => x.Translations)
                            .Where(x => x.DeletedAt == null)
                            .OrderByDescending(x => x.Date)
                            .ToListAsync();
            List<MonetaryIndicatorGetDTO> data = _mapper.Map<List<MonetaryIndicatorGetDTO>>(entities);
            foreach (MonetaryIndicatorGetDTO item in data)
            {
                MonetaryIndicator? entity = entities.FirstOrDefault(x => x.Id == item.Id);
                item.PercentCategoryTitle = entity?.MonetaryIndicatorCategory?.Translations.Where(x => x.LanguageId == 1).FirstOrDefault()?.Title;
            }
            return data;
        }

        public async Task<MonetaryIndicatorGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null)
                return null;

            return _mapper.Map<MonetaryIndicatorGetDTO>(entity);
        }

        public async Task<bool> CreateAsync(MonetaryIndicatorCreateDTO dto)
        {
            var entity = _mapper.Map<MonetaryIndicator>(dto);
            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, MonetaryIndicatorEditDTO dto)
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

        public async Task<MonetaryIndicatorEditDTO?> GetForEditAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity is null ? null : _mapper.Map<MonetaryIndicatorEditDTO>(entity);
        }
    }
}
