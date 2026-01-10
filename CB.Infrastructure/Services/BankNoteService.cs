
using AutoMapper;
using CB.Application.DTOs.BankNote;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class BankNoteService : IBankNoteService
    {
        private readonly IGenericRepository<BankNote> _repository;
        private readonly IMapper _mapper;

        public BankNoteService(IGenericRepository<BankNote> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<BankNoteGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                            .Include(x => x.BankNoteCategory)
                            .ThenInclude(x => x.Translations)
                            .Where(x => x.DeletedAt == null)
                            .OrderByDescending(x => x.Date)
                            .ToListAsync();
            List<BankNoteGetDTO> data = _mapper.Map<List<BankNoteGetDTO>>(entities);
            foreach (BankNoteGetDTO item in data)
            {
                BankNote? entity = entities.FirstOrDefault(x => x.Id == item.Id);
                item.PercentCategoryTitle = entity?.BankNoteCategory?.Translations.Where(x => x.LanguageId == 1).FirstOrDefault()?.Title;
            }
            return data;
        }

        public async Task<BankNoteGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null)
                return null;

            return _mapper.Map<BankNoteGetDTO>(entity);
        }

        public async Task<bool> CreateAsync(BankNoteCreateDTO dto)
        {
            var entity = _mapper.Map<BankNote>(dto);
            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, BankNoteEditDTO dto)
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

        public async Task<BankNoteEditDTO?> GetForEditAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity is null ? null : _mapper.Map<BankNoteEditDTO>(entity);
        }
    }
}
