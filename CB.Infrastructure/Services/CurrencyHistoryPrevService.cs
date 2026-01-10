
using AutoMapper;
using CB.Application.DTOs.CurrencyHistoryPrev;
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
    public class CurrencyHistoryPrevService : ICurrencyHistoryPrevService
    {
        private readonly IGenericRepository<CurrencyHistoryPrev> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

        public CurrencyHistoryPrevService(
            IGenericRepository<CurrencyHistoryPrev> repository,
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

        public async Task<List<CurrencyHistoryPrevGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(x => x.CurrencyHistory)
                        .ThenInclude(x => x.Translations)
                        .ToListAsync();

            List<CurrencyHistoryPrevGetDTO> data = _mapper.Map<List<CurrencyHistoryPrevGetDTO>>(entities);
            foreach (CurrencyHistoryPrevGetDTO item in data)
            {
                var entity = entities.FirstOrDefault(x => x.Id == item.Id);
                if (entity is null) continue;
                item.Image = $"{_config["BaseUrl"]}{entity.Image}";
                item.CurrencyHistoryTitle = entity.CurrencyHistory.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            }
            return data;
        }
        public async Task<CurrencyHistoryPrevGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(x => x.CurrencyHistory)
                        .ThenInclude(x => x.Translations)
                        .FirstOrDefaultAsync(b => b.Id == id);

            if (entity is null) return null;

            CurrencyHistoryPrevGetDTO? data = _mapper.Map<CurrencyHistoryPrevGetDTO>(entity);
            data.Image = $"{_config["BaseUrl"]}{entity.Image}";
            data.CurrencyHistoryTitle = entity.CurrencyHistory.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            return data;
        }
        public async Task<bool> CreateAsync(CurrencyHistoryPrevCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<CurrencyHistoryPrev>(dto);
            entity.Image = await dto.File.FileUpload(_env.WebRootPath, "currency-histories");
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.SubTitles.TryGetValue(t.Key, out var subTitle);

                return new CurrencyHistoryPrevTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    SubTitle = subTitle
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, CurrencyHistoryPrevEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            if (dto.File != null)
            {
                FileManager.FileDelete(_env.WebRootPath, entity.Image ?? "");
                entity.Image = await dto.File.FileUpload(_env.WebRootPath, "currency-histories");
            }

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.SubTitles.TryGetValue(t.Key, out var subTitle);

                return new CurrencyHistoryPrevTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    SubTitle = subTitle
                };
            }).ToList();



            return await _repository.UpdateAsync(entity);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null) return false;
            FileManager.FileDelete(_env.WebRootPath, entity.Image ?? "");
            return await _repository.DeleteAsync(entity);
        }

    }
}
