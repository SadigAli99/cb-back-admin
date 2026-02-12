
using AutoMapper;
using CB.Application.DTOs.InstantPaymentOrganization;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class InstantPaymentOrganizationService : IInstantPaymentOrganizationService
    {
        private readonly IGenericRepository<InstantPaymentOrganization> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public InstantPaymentOrganizationService(
            IGenericRepository<InstantPaymentOrganization> repository,
            IGenericRepository<Language> languageRepository,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<List<InstantPaymentOrganizationGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<InstantPaymentOrganizationGetDTO> data = _mapper.Map<List<InstantPaymentOrganizationGetDTO>>(entities);
            return data;
        }
        public async Task<InstantPaymentOrganizationGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            InstantPaymentOrganizationGetDTO? data = entity is null ? null : _mapper.Map<InstantPaymentOrganizationGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(InstantPaymentOrganizationCreateDTO dto)
        {

            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<InstantPaymentOrganization>(dto);
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Descriptions.TryGetValue(t.Key, out var description);

                return new InstantPaymentOrganizationTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Description = description,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, InstantPaymentOrganizationEditDTO dto)
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

                dto.Descriptions.TryGetValue(t.Key, out var description);

                return new InstantPaymentOrganizationTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Description = description
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
