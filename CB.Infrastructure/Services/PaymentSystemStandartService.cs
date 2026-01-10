
using AutoMapper;
using CB.Application.DTOs.PaymentSystemStandart;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class PaymentSystemStandartService : IPaymentSystemStandartService
    {
        private readonly IGenericRepository<PaymentSystemStandart> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public PaymentSystemStandartService(
            IGenericRepository<PaymentSystemStandart> repository,
            IGenericRepository<Language> languageRepository,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<List<PaymentSystemStandartGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<PaymentSystemStandartGetDTO> data = _mapper.Map<List<PaymentSystemStandartGetDTO>>(entities);
            return data;
        }
        public async Task<PaymentSystemStandartGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            PaymentSystemStandartGetDTO? data = entity is null ? null : _mapper.Map<PaymentSystemStandartGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(PaymentSystemStandartCreateDTO dto)
        {

            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<PaymentSystemStandart>(dto);
            dto.Slugs = SlugHelper.GenerateSlugs(dto.Titles);
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Slugs.TryGetValue(t.Key, out var slug);
                dto.Descriptions.TryGetValue(t.Key, out var description);

                return new PaymentSystemStandartTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Slug = slug ?? string.Empty,
                    Description = description ?? string.Empty,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, PaymentSystemStandartEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            var languages = await _languageRepository.GetAllAsync();

            dto.Slugs = SlugHelper.GenerateSlugs(dto.Titles);

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Slugs.TryGetValue(t.Key, out var slug);
                dto.Descrtiptions.TryGetValue(t.Key, out var description);

                return new PaymentSystemStandartTranslation
                {
                    LanguageId = lang.Id,
                    Slug = slug,
                    Title = t.Value,
                    Description = description ?? string.Empty
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
