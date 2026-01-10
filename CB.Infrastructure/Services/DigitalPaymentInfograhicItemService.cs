
using AutoMapper;
using CB.Application.DTOs.DigitalPaymentInfograhicItem;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class DigitalPaymentInfograhicItemService : IDigitalPaymentInfograhicItemService
    {
        private readonly IGenericRepository<DigitalPaymentInfograhicItem> _repository;
        private readonly IMapper _mapper;

        public DigitalPaymentInfograhicItemService(
            IGenericRepository<DigitalPaymentInfograhicItem> repository,
            IMapper mapper
        )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<DigitalPaymentInfograhicItemGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(x => x.DigitalPaymentInfograhic)
                        .ThenInclude(x => x.Translations)
                        .ToListAsync();

            List<DigitalPaymentInfograhicItemGetDTO> data = _mapper.Map<List<DigitalPaymentInfograhicItemGetDTO>>(entities);

            foreach (var item in data)
            {
                var entity = entities.FirstOrDefault(x => x.Id == item.Id);
                if (entity is null) continue;
                item.DigitalPaymentInfograhicTitle = entity.DigitalPaymentInfograhic?.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            }
            return data;
        }
        public async Task<DigitalPaymentInfograhicItemGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(x => x.DigitalPaymentInfograhic)
                        .ThenInclude(x => x.Translations)
                        .FirstOrDefaultAsync(b => b.Id == id);

            if (entity is null) return null;
            DigitalPaymentInfograhicItemGetDTO? data = _mapper.Map<DigitalPaymentInfograhicItemGetDTO>(entity);
            data.DigitalPaymentInfograhicTitle = entity.DigitalPaymentInfograhic?.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            return data;
        }
        public async Task<bool> CreateAsync(DigitalPaymentInfograhicItemCreateDTO dto)
        {

            var entity = _mapper.Map<DigitalPaymentInfograhicItem>(dto);
            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, DigitalPaymentInfograhicItemEditDTO dto)
        {
            var entity = await _repository.GetQuery().FirstOrDefaultAsync(x => x.Id == id);

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
