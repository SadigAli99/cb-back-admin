
using AutoMapper;
using CB.Application.DTOs.InsuranceStatistic;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class InsuranceStatisticService : IInsuranceStatisticService
    {
        private readonly IGenericRepository<InsuranceStatistic> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public InsuranceStatisticService(
            IGenericRepository<InsuranceStatistic> repository,
            IGenericRepository<Language> languageRepository,
            IWebHostEnvironment env,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
            _env = env;
        }

        public async Task<List<InsuranceStatisticGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(x => x.InsuranceStatisticCategory)
                        .ThenInclude(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .Include(x => x.InsuranceStatisticSubCategory)
                        .ThenInclude(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .ToListAsync();

            List<InsuranceStatisticGetDTO> data = _mapper.Map<List<InsuranceStatisticGetDTO>>(entities);

            foreach (var item in data)
            {
                var entity = entities.FirstOrDefault(x => x.Id == item.Id);
                if (entity is null) continue;
                item.InsuranceStatisticCategory = entity.InsuranceStatisticCategory?.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
                item.InsuranceStatisticSubCategory = entity.InsuranceStatisticSubCategory?.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            }
            return data;
        }
        public async Task<InsuranceStatisticGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(x => x.InsuranceStatisticCategory)
                        .ThenInclude(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .Include(x => x.InsuranceStatisticSubCategory)
                        .ThenInclude(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            if (entity is null) return null;

            InsuranceStatisticGetDTO? data = _mapper.Map<InsuranceStatisticGetDTO>(entity);
            data.InsuranceStatisticCategory = entity.InsuranceStatisticCategory?.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            data.InsuranceStatisticSubCategory = entity.InsuranceStatisticSubCategory?.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;

            return data;
        }
        public async Task<bool> CreateAsync(InsuranceStatisticCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<InsuranceStatistic>(dto);
            entity.File = await dto.File.FileUpload(_env.WebRootPath, "insurance-statistics");
            entity.FileType = Path.GetExtension(dto.File.FileName)?.TrimStart('.');
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.CoverTitles.TryGetValue(t.Key, out var coverTitle);


                return new InsuranceStatisticTranslation
                {
                    LanguageId = lang.Id,
                    CoverTitle = coverTitle,
                    Title = t.Value,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, InsuranceStatisticEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            if (dto.File != null)
            {
                FileManager.FileDelete(_env.WebRootPath, entity.File ?? "");
                entity.File = await dto.File.FileUpload(_env.WebRootPath, "insurance-statistics");
                entity.FileType = Path.GetExtension(dto.File.FileName)?.TrimStart('.');
            }

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);

                dto.CoverTitles.TryGetValue(t.Key, out var coverTitle);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                return new InsuranceStatisticTranslation
                {
                    LanguageId = lang.Id,
                    CoverTitle = coverTitle,
                    Title = t.Value,
                };
            }).ToList();



            return await _repository.UpdateAsync(entity);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null) return false;
            FileManager.FileDelete(_env.WebRootPath, entity.File ?? "");
            return await _repository.DeleteAsync(entity);
        }

    }
}
