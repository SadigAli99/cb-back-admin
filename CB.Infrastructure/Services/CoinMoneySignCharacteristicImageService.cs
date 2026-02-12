
using AutoMapper;
using CB.Application.DTOs.CoinMoneySignCharacteristicImage;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CB.Infrastructure.Services
{
    public class CoinMoneySignCharacteristicImageService : ICoinMoneySignCharacteristicImageService
    {
        private readonly IGenericRepository<MoneySignCharacteristicImage> _repository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public CoinMoneySignCharacteristicImageService(
            IGenericRepository<MoneySignCharacteristicImage> repository,
            IFileService fileService,
            IConfiguration config,
            IMapper mapper
        )
        {
            _fileService = fileService;
            _repository = repository;
            _mapper = mapper;
            _config = config;
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
            entity.FrontImage = await _fileService.UploadAsync(dto.FrontFile, "money-sign-characteristics");
            entity.BackImage = await _fileService.UploadAsync(dto.BackFile, "money-sign-characteristics");
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
                _fileService.Delete(entity.FrontImage);
                entity.FrontImage = await _fileService.UploadAsync(dto.FrontFile, "money-sign-characteristics");
            }

            if (dto.BackFile != null)
            {
                _fileService.Delete(entity.BackImage);
                entity.BackImage = await _fileService.UploadAsync(dto.BackFile, "money-sign-characteristics");
            }


            return await _repository.UpdateAsync(entity);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null) return false;
            _fileService.Delete(entity.FrontImage);
            _fileService.Delete(entity.BackImage);
            return await _repository.DeleteAsync(entity);
        }

    }
}
