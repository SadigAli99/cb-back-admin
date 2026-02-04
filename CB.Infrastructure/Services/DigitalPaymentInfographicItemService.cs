
using AutoMapper;
using CB.Application.DTOs.DigitalPaymentInfographicItem;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class DigitalPaymentInfographicItemService : IDigitalPaymentInfographicItemService
    {
        private readonly IGenericRepository<DigitalPaymentInfographicItem> _repository;
        private readonly IMapper _mapper;

        public DigitalPaymentInfographicItemService(
            IGenericRepository<DigitalPaymentInfographicItem> repository,
            IMapper mapper
        )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<DigitalPaymentInfographicItemGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(x => x.DigitalPaymentInfographic)
                        .ThenInclude(x => x.Translations)
                        .ToListAsync();

            List<DigitalPaymentInfographicItemGetDTO> data = _mapper.Map<List<DigitalPaymentInfographicItemGetDTO>>(entities);

            foreach (var item in data)
            {
                var entity = entities.FirstOrDefault(x => x.Id == item.Id);
                if (entity is null) continue;
                item.DigitalPaymentInfographicTitle = entity.DigitalPaymentInfographic?.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            }
            return data;
        }
        public async Task<DigitalPaymentInfographicItemGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(x => x.DigitalPaymentInfographic)
                        .ThenInclude(x => x.Translations)
                        .FirstOrDefaultAsync(b => b.Id == id);

            if (entity is null) return null;
            DigitalPaymentInfographicItemGetDTO? data = _mapper.Map<DigitalPaymentInfographicItemGetDTO>(entity);
            data.DigitalPaymentInfographicTitle = entity.DigitalPaymentInfographic?.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            return data;
        }
        public async Task<bool> CreateAsync(DigitalPaymentInfographicItemCreateDTO dto)
        {

            var entity = _mapper.Map<DigitalPaymentInfographicItem>(dto);
            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, DigitalPaymentInfographicItemEditDTO dto)
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
