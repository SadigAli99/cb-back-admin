
using AutoMapper;
using CB.Application.DTOs.CurrencyHistoryNextItem;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CB.Infrastructure.Services
{
    public class CurrencyHistoryNextItemService : ICurrencyHistoryNextItemService
    {
        private readonly IGenericRepository<CurrencyHistoryNextItem> _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

        public CurrencyHistoryNextItemService(
            IGenericRepository<CurrencyHistoryNextItem> repository,
            IWebHostEnvironment env,
            IConfiguration config,
            IMapper mapper
        )
        {
            _repository = repository;
            _mapper = mapper;
            _config = config;
            _env = env;
        }

        public async Task<List<CurrencyHistoryNextItemGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(x => x.CurrencyHistoryNext)
                        .ThenInclude(x => x.Translations)
                        .ToListAsync();

            List<CurrencyHistoryNextItemGetDTO> data = _mapper.Map<List<CurrencyHistoryNextItemGetDTO>>(entities);
            foreach (CurrencyHistoryNextItemGetDTO item in data)
            {
                var entity = entities.FirstOrDefault(x => x.Id == item.Id);
                if (entity is null) continue;
                item.CurrencyHistoryNextTitle = entity.CurrencyHistoryNext.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
                item.FrontImage = $"{_config["BaseUrl"]}{entity.FrontImage}";
                item.BackImage = $"{_config["BaseUrl"]}{entity.BackImage}";
            }
            return data;
        }
        public async Task<CurrencyHistoryNextItemGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(x => x.CurrencyHistoryNext)
                        .ThenInclude(x => x.Translations)
                        .FirstOrDefaultAsync(b => b.Id == id);

            if (entity is null) return null;

            CurrencyHistoryNextItemGetDTO? data = _mapper.Map<CurrencyHistoryNextItemGetDTO>(entity);
            data.CurrencyHistoryNextTitle = entity.CurrencyHistoryNext.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            data.FrontImage = $"{_config["BaseUrl"]}{entity.FrontImage}";
            data.BackImage = $"{_config["BaseUrl"]}{entity.BackImage}";
            return data;
        }
        public async Task<bool> CreateAsync(CurrencyHistoryNextItemCreateDTO dto)
        {
            var entity = _mapper.Map<CurrencyHistoryNextItem>(dto);
            entity.FrontImage = await dto.FrontFile.FileUpload(_env.WebRootPath, "currency-histories");
            entity.BackImage = await dto.BackFile.FileUpload(_env.WebRootPath, "currency-histories");

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, CurrencyHistoryNextItemEditDTO dto)
        {
            var entity = await _repository.GetQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            if (dto.FrontFile != null)
            {
                FileManager.FileDelete(_env.WebRootPath, entity.FrontImage ?? "");
                entity.FrontImage = await dto.FrontFile.FileUpload(_env.WebRootPath, "currency-histories");
            }

            if (dto.BackFile != null)
            {
                FileManager.FileDelete(_env.WebRootPath, entity.BackImage ?? "");
                entity.BackImage = await dto.BackFile.FileUpload(_env.WebRootPath, "currency-histories");
            }


            return await _repository.UpdateAsync(entity);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null) return false;
            FileManager.FileDelete(_env.WebRootPath, entity.FrontImage ?? "");
            FileManager.FileDelete(_env.WebRootPath, entity.BackImage ?? "");
            return await _repository.DeleteAsync(entity);
        }

    }
}
