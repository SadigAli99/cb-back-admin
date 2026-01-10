using System.Globalization;
using AutoMapper;
using CB.Application.DTOs.Inflation;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;

namespace CB.Infrastructure.Services
{
    public class InflationService : IInflationService
    {
        private readonly IGenericRepository<Inflation> _repository;
        private readonly IMapper _mapper;

        public InflationService(IGenericRepository<Inflation> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<InflationGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            List<InflationGetDTO> data = _mapper.Map<List<InflationGetDTO>>(entities);
            foreach (InflationGetDTO item in data)
            {
                var entity = entities.FirstOrDefault(x => x.Id == item.Id);
                string[] months = GetOXData("az");
                item.Month = item.Month;
            }

            return data;
        }

        public async Task<InflationGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null)
                return null;

            return _mapper.Map<InflationGetDTO>(entity);
        }

        public async Task<bool> CreateAsync(InflationCreateDTO dto)
        {
            var entity = _mapper.Map<Inflation>(dto);
            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, InflationEditDTO dto)
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

        public async Task<InflationEditDTO?> GetForEditAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity is null ? null : _mapper.Map<InflationEditDTO>(entity);
        }

        public string[] GetOXData(string lang)
        {
            var culture = lang switch
            {
                "az" => new CultureInfo("az-Latn-AZ"),
                "en" => new CultureInfo("en-US"),
                _ => new CultureInfo("az-Latn-AZ")
            };

            string[] months = culture.DateTimeFormat.AbbreviatedMonthNames;

            return months.Take(12)
                         .Select(m => char.ToUpper(m[0]) + m.Substring(1).ToLower())
                         .ToArray();
        }


        public int[] GetOYData()
        {
            double? max = _repository.GetQuery()
                .Select(x => (double?)x.Value)
                .Max();

            if (max == null)
                return Array.Empty<int>();

            int maxInt = (int)Math.Ceiling(max.Value);

            if (maxInt % 2 != 0)
                maxInt++;

            return Enumerable.Range(0, (maxInt / 2) + 1)
                             .Select(x => x * 2)
                             .ToArray();
        }

    }
}
