
using AutoMapper;
using CB.Application.DTOs.InsuranceStatisticSubCategory;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class InsuranceStatisticSubCategoryService : IInsuranceStatisticSubCategoryService
    {
        private readonly IGenericRepository<InsuranceStatisticSubCategory> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public InsuranceStatisticSubCategoryService(
            IGenericRepository<InsuranceStatisticSubCategory> repository,
            IGenericRepository<Language> languageRepository,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<List<InsuranceStatisticSubCategoryGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(x => x.InsuranceStatisticCategory)
                        .ThenInclude(x => x.Translations)
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<InsuranceStatisticSubCategoryGetDTO> data = _mapper.Map<List<InsuranceStatisticSubCategoryGetDTO>>(entities);
            foreach (var item in data)
            {
                var entity = entities.FirstOrDefault(x => x.Id == item.Id);
                item.InsuranceStatisticCategory = entity?.InsuranceStatisticCategory?.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            }
            return data;
        }
        public async Task<InsuranceStatisticSubCategoryGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            InsuranceStatisticSubCategoryGetDTO? data = entity is null ? null : _mapper.Map<InsuranceStatisticSubCategoryGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(InsuranceStatisticSubCategoryCreateDTO dto)
        {

            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<InsuranceStatisticSubCategory>(dto);
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                return new InsuranceStatisticSubCategoryTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, InsuranceStatisticSubCategoryEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                return new InsuranceStatisticSubCategoryTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value
                };
            }).ToList();

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
