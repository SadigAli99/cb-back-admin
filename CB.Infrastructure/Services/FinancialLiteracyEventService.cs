
using AutoMapper;
using CB.Application.DTOs.FinancialLiteracyEvent;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class FinancialLiteracyEventService : IFinancialLiteracyEventService
    {
        private readonly IGenericRepository<FinancialLiteracyEvent> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public FinancialLiteracyEventService(
            IGenericRepository<FinancialLiteracyEvent> repository,
            IGenericRepository<Language> languageRepository,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<List<FinancialLiteracyEventGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<FinancialLiteracyEventGetDTO> data = _mapper.Map<List<FinancialLiteracyEventGetDTO>>(entities);
            return data;
        }
        public async Task<FinancialLiteracyEventGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            FinancialLiteracyEventGetDTO? data = entity is null ? null : _mapper.Map<FinancialLiteracyEventGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(FinancialLiteracyEventCreateDTO dto)
        {

            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<FinancialLiteracyEvent>(dto);
            entity.Translations = dto.Descriptions.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");


                return new FinancialLiteracyEventTranslation
                {
                    LanguageId = lang.Id,
                    Description = t.Value,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, FinancialLiteracyEventEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Descriptions.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");
                dto.Descriptions.TryGetValue(t.Key, out var description);


                return new FinancialLiteracyEventTranslation
                {
                    LanguageId = lang.Id,
                    Description = t.Value,
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
