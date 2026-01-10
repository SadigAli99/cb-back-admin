
using AutoMapper;
using CB.Application.DTOs.CoinMoneySignCharacteristicImage;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Core.Enums;
using CB.Shared.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CB.Infrastructure.Services
{
    public class CoinMoneySignCharacteristicImageService : ICoinMoneySignCharacteristicImageService
    {
        private readonly IGenericRepository<MoneySignCharacteristicImage> _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

        public CoinMoneySignCharacteristicImageService(
            IGenericRepository<MoneySignCharacteristicImage> repository,
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

        public async Task<List<CoinMoneySignCharacteristicImageGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(x => x.MoneySignHistory)
                        .ThenInclude(x => x.Translations)
                        .Include(x => x.MoneySignHistory)
                        .ThenInclude(x => x.MoneySign)
                        .Where(x => x.MoneySignHistory.MoneySign.Type == MoneySignType.COIN)
                        .ToListAsync();

            List<CoinMoneySignCharacteristicImageGetDTO> data = _mapper.Map<List<CoinMoneySignCharacteristicImageGetDTO>>(entities);
            foreach (CoinMoneySignCharacteristicImageGetDTO item in data)
            {
                var entity = entities.FirstOrDefault(x => x.Id == item.Id);
                if (entity is null) continue;
                item.FrontImage = $"{_config["BaseUrl"]}{entity.FrontImage}";
                item.BackImage = $"{_config["BaseUrl"]}{entity.BackImage}";
                item.MoneySignHistoryTitle = entity.MoneySignHistory.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            }
            return data;
        }
        public async Task<CoinMoneySignCharacteristicImageGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(x => x.MoneySignHistory)
                        .ThenInclude(x => x.Translations)
                        .Include(x => x.MoneySignHistory)
                        .ThenInclude(x => x.MoneySign)
                        .Where(x => x.MoneySignHistory.MoneySign.Type == MoneySignType.COIN)
                        .FirstOrDefaultAsync(b => b.Id == id);

            if (entity is null) return null;

            CoinMoneySignCharacteristicImageGetDTO? data = _mapper.Map<CoinMoneySignCharacteristicImageGetDTO>(entity);
            data.FrontImage = $"{_config["BaseUrl"]}{entity.FrontImage}";
            data.BackImage = $"{_config["BaseUrl"]}{entity.BackImage}";
            data.MoneySignHistoryTitle = entity.MoneySignHistory.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            return data;
        }
        public async Task<bool> CreateAsync(CoinMoneySignCharacteristicImageCreateDTO dto)
        {
            var entity = _mapper.Map<MoneySignCharacteristicImage>(dto);
            entity.FrontImage = await dto.FrontFile.FileUpload(_env.WebRootPath, "money-sign-characteristics");
            entity.BackImage = await dto.BackFile.FileUpload(_env.WebRootPath, "money-sign-characteristics");
            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, CoinMoneySignCharacteristicImageEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            if (dto.FrontFile != null)
            {
                FileManager.FileDelete(_env.WebRootPath, entity.FrontImage ?? "");
                entity.FrontImage = await dto.FrontFile.FileUpload(_env.WebRootPath, "money-sign-characteristics");
            }

            if (dto.BackFile != null)
            {
                FileManager.FileDelete(_env.WebRootPath, entity.BackImage ?? "");
                entity.BackImage = await dto.BackFile.FileUpload(_env.WebRootPath, "money-sign-characteristics");
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
