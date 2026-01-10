
using AutoMapper;
using CB.Application.DTOs.BankSector;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class BankSectorService : IBankSectorService
    {
        private readonly IGenericRepository<BankSector> _repository;
        private readonly IMapper _mapper;

        public BankSectorService(IGenericRepository<BankSector> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<BankSectorGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                            .Include(x => x.BankSectorCategory)
                            .ThenInclude(x => x.Translations)
                            .Where(x => x.DeletedAt == null)
                            .OrderByDescending(x => x.Date)
                            .ToListAsync();
            List<BankSectorGetDTO> data = _mapper.Map<List<BankSectorGetDTO>>(entities);
            foreach (BankSectorGetDTO item in data)
            {
                BankSector? entity = entities.FirstOrDefault(x => x.Id == item.Id);
                item.PercentCategoryTitle = entity?.BankSectorCategory?.Translations.Where(x => x.LanguageId == 1).FirstOrDefault()?.Title;
            }
            return data;
        }

        public async Task<BankSectorGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null)
                return null;

            return _mapper.Map<BankSectorGetDTO>(entity);
        }

        public async Task<bool> CreateAsync(BankSectorCreateDTO dto)
        {
            var entity = _mapper.Map<BankSector>(dto);
            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, BankSectorEditDTO dto)
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

        public async Task<BankSectorEditDTO?> GetForEditAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity is null ? null : _mapper.Map<BankSectorEditDTO>(entity);
        }
    }
}
