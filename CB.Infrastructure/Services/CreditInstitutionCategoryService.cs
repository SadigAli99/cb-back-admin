
using AutoMapper;
using CB.Application.DTOs.CreditInstitutionCategory;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class CreditInstitutionCategoryService : ICreditInstitutionCategoryService
    {
        private readonly IGenericRepository<CreditInstitutionCategory> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public CreditInstitutionCategoryService(
            IGenericRepository<CreditInstitutionCategory> repository,
            IGenericRepository<Language> languageRepository,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<List<CreditInstitutionCategoryGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<CreditInstitutionCategoryGetDTO> data = _mapper.Map<List<CreditInstitutionCategoryGetDTO>>(entities);
            return data;
        }
        public async Task<CreditInstitutionCategoryGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            CreditInstitutionCategoryGetDTO? data = entity is null ? null : _mapper.Map<CreditInstitutionCategoryGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(CreditInstitutionCategoryCreateDTO dto)
        {

            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<CreditInstitutionCategory>(dto);
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                return new CreditInstitutionCategoryTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, CreditInstitutionCategoryEditDTO dto)
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

                return new CreditInstitutionCategoryTranslation
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
