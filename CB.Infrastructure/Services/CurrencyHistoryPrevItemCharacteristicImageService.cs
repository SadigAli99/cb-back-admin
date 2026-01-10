
using AutoMapper;
using CB.Application.DTOs.CurrencyHistoryPrevItemCharacteristicImage;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CB.Infrastructure.Services
{
    public class CurrencyHistoryPrevItemCharacteristicImageService : ICurrencyHistoryPrevItemCharacteristicImageService
    {
        private readonly IGenericRepository<CurrencyHistoryPrevItemCharacteristicImage> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

        public CurrencyHistoryPrevItemCharacteristicImageService(
            IGenericRepository<CurrencyHistoryPrevItemCharacteristicImage> repository,
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

        public async Task<List<CurrencyHistoryPrevItemCharacteristicImageGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(x => x.CurrencyHistoryPrev)
                        .ThenInclude(x => x.Translations)
                        .ToListAsync();

            List<CurrencyHistoryPrevItemCharacteristicImageGetDTO> data = _mapper.Map<List<CurrencyHistoryPrevItemCharacteristicImageGetDTO>>(entities);
            foreach (CurrencyHistoryPrevItemCharacteristicImageGetDTO item in data)
            {
                var entity = entities.FirstOrDefault(x => x.Id == item.Id);
                if (entity is null) continue;
                item.CurrencyHistoryPrevTitle = entity.CurrencyHistoryPrev.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
                item.FrontImage = $"{_config["BaseUrl"]}{entity.FrontImage}";
                item.BackImage = $"{_config["BaseUrl"]}{entity.BackImage}";
            }
            return data;
        }
        public async Task<CurrencyHistoryPrevItemCharacteristicImageGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(x => x.CurrencyHistoryPrev)
                        .ThenInclude(x => x.Translations)
                        .FirstOrDefaultAsync(b => b.Id == id);

            if (entity is null) return null;

            CurrencyHistoryPrevItemCharacteristicImageGetDTO? data = _mapper.Map<CurrencyHistoryPrevItemCharacteristicImageGetDTO>(entity);
            data.FrontImage = $"{_config["BaseUrl"]}{entity.FrontImage}";
            data.BackImage = $"{_config["BaseUrl"]}{entity.BackImage}";
            return data;
        }
        public async Task<bool> CreateAsync(CurrencyHistoryPrevItemCharacteristicImageCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<CurrencyHistoryPrevItemCharacteristicImage>(dto);
            entity.FrontImage = await dto.FrontFile.FileUpload(_env.WebRootPath, "currency-histories");
            entity.BackImage = await dto.BackFile.FileUpload(_env.WebRootPath, "currency-histories");
            entity.Translations = dto.Colors.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Sizes.TryGetValue(t.Key, out var size);

                return new CurrencyHistoryPrevItemCharacteristicImageTranslation
                {
                    LanguageId = lang.Id,
                    Color = t.Value,
                    Size = size
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, CurrencyHistoryPrevItemCharacteristicImageEditDTO dto)
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
                entity.FrontImage = await dto.FrontFile.FileUpload(_env.WebRootPath, "currency-histories");
            }

            if (dto.BackFile != null)
            {
                FileManager.FileDelete(_env.WebRootPath, entity.BackImage ?? "");
                entity.BackImage = await dto.BackFile.FileUpload(_env.WebRootPath, "currency-histories");
            }

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Colors.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Sizes.TryGetValue(t.Key, out var size);

                return new CurrencyHistoryPrevItemCharacteristicImageTranslation
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
