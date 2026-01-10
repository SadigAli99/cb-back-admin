
using AutoMapper;
using CB.Application.DTOs.OutOfCoinMoneySignCharacteristicImage;
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
    public class OutOfCoinMoneySignCharacteristicImageService : IOutOfCoinMoneySignCharacteristicImageService
    {
        private readonly IGenericRepository<MoneySignCharacteristicImage> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

        public OutOfCoinMoneySignCharacteristicImageService(
            IGenericRepository<MoneySignCharacteristicImage> repository,
            IGenericRepository<Language> languageRepository,
            IWebHostEnvironment env,
            IConfiguration config,
            IMapper mapper
        )
        {
            _languageRepository = languageRepository;
            _repository = repository;
            _mapper = mapper;
            _config = config;
            _env = env;
        }

        public async Task<List<OutOfCoinMoneySignCharacteristicImageGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(x => x.MoneySignHistory)
                        .ThenInclude(x => x.Translations)
                        .Include(x => x.MoneySignHistory)
                        .ThenInclude(x => x.MoneySign)
                        .Where(x => x.MoneySignHistory.MoneySign.Type == MoneySignType.COINOUT)
                        .ToListAsync();

            List<OutOfCoinMoneySignCharacteristicImageGetDTO> data = _mapper.Map<List<OutOfCoinMoneySignCharacteristicImageGetDTO>>(entities);
            foreach (OutOfCoinMoneySignCharacteristicImageGetDTO item in data)
            {
                var entity = entities.FirstOrDefault(x => x.Id == item.Id);
                if (entity is null) continue;
                item.FrontImage = $"{_config["BaseUrl"]}{entity.FrontImage}";
                item.BackImage = $"{_config["BaseUrl"]}{entity.BackImage}";
                item.MoneySignHistoryTitle = entity.MoneySignHistory.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            }
            return data;
        }
        public async Task<OutOfCoinMoneySignCharacteristicImageGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(x => x.MoneySignHistory)
                        .ThenInclude(x => x.Translations)
                        .Include(x => x.MoneySignHistory)
                        .ThenInclude(x => x.MoneySign)
                        .Where(x => x.MoneySignHistory.MoneySign.Type == MoneySignType.COINOUT)
                        .FirstOrDefaultAsync(b => b.Id == id);

            if (entity is null) return null;

            OutOfCoinMoneySignCharacteristicImageGetDTO? data = _mapper.Map<OutOfCoinMoneySignCharacteristicImageGetDTO>(entity);
            data.FrontImage = $"{_config["BaseUrl"]}{entity.FrontImage}";
            data.BackImage = $"{_config["BaseUrl"]}{entity.BackImage}";
            data.MoneySignHistoryTitle = entity.MoneySignHistory.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            return data;
        }
        public async Task<bool> CreateAsync(OutOfCoinMoneySignCharacteristicImageCreateDTO dto)
        {
            var entity = _mapper.Map<MoneySignCharacteristicImage>(dto);
            entity.FrontImage = await dto.FrontFile.FileUpload(_env.WebRootPath, "money-sign-characteristics");
            entity.BackImage = await dto.BackFile.FileUpload(_env.WebRootPath, "money-sign-characteristics");


            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Colors.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Sizes.TryGetValue(t.Key, out var size);

                return new MoneySignCharacteristicImageTranslation
                {
                    LanguageId = lang.Id,
                    Color = t.Value,
                    Size = size
                };
            }).ToList();
            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, OutOfCoinMoneySignCharacteristicImageEditDTO dto)
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


            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Colors.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Sizes.TryGetValue(t.Key, out var size);

                return new MoneySignCharacteristicImageTranslation
                {
                    LanguageId = lang.Id,
                    Color = t.Value,
                    Size = size
                };
            }).ToList();


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
