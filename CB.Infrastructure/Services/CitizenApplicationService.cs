
using AutoMapper;
using CB.Application.DTOs.CitizenApplication;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class CitizenApplicationService : ICitizenApplicationService
    {
        private readonly IGenericRepository<CitizenApplication> _repository;
        private readonly IMapper _mapper;

        public CitizenApplicationService(IGenericRepository<CitizenApplication> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CitizenApplicationGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                            .Include(x => x.CitizenApplicationCategory)
                            .ThenInclude(x => x.Translations)
                            .ToListAsync();
            List<CitizenApplicationGetDTO> data = _mapper.Map<List<CitizenApplicationGetDTO>>(entities);
            foreach (CitizenApplicationGetDTO item in data)
            {
                CitizenApplication? entity = entities.FirstOrDefault(x => x.Id == item.Id);
                item.CitizenApplicationCategoryTitle = entity?.CitizenApplicationCategory?.Translations.Where(x => x.LanguageId == 1).FirstOrDefault()?.Title;
            }
            return data;
        }

        public async Task<CitizenApplicationGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null)
                return null;

            var data = _mapper.Map<CitizenApplicationGetDTO>(entity);
            data.CitizenApplicationCategoryTitle = entity?.CitizenApplicationCategory?
                                            .Translations
                                            .Where(x => x.LanguageId == 1)
                                            .FirstOrDefault()?.Title;

            return data;

        }

        public async Task<bool> CreateAsync(CitizenApplicationCreateDTO dto)
        {
            var entity = _mapper.Map<CitizenApplication>(dto);
            entity.TotalCount = dto.CapitalMarketCount +
                                dto.CreditInstitutionCount +
                                dto.CurrencyExchangeCount +
                                dto.InsurerCount +
                                dto.PaymentSystemCount +
                                dto.OtherCount;
            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, CitizenApplicationEditDTO dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null)
                return false;

            _mapper.Map(dto, entity);
            entity.TotalCount = dto.CapitalMarketCount +
                                dto.CreditInstitutionCount +
                                dto.CurrencyExchangeCount +
                                dto.InsurerCount +
                                dto.PaymentSystemCount +
                                dto.OtherCount;
            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null)
                return false;


            return await _repository.DeleteAsync(entity); ;
        }

    }
}
