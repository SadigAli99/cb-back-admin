
using AutoMapper;
using CB.Application.DTOs.PaymentSystemControlService;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using Service = CB.Core.Entities.PaymentSystemControlService;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class PaymentSystemControlServiceService : IPaymentSystemControlServiceService
    {
        private readonly IGenericRepository<Service> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public PaymentSystemControlServiceService(
            IGenericRepository<Service> repository,
            IGenericRepository<Language> languageRepository,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<List<PaymentSystemControlServiceGetDTO>> GetAllAsync(int? id)
        {
            var query = _repository.GetQuery();

            if (id != default(int)) query = query.Where(x => x.PaymentSystemControlId == id);

            var entities = await query
                        .Include(x => x.PaymentSystemControl)
                        .ThenInclude(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();


            List<PaymentSystemControlServiceGetDTO> data = _mapper.Map<List<PaymentSystemControlServiceGetDTO>>(entities);

            foreach (var entity in entities)
            {
                PaymentSystemControlServiceGetDTO? dTO = data.FirstOrDefault(x => x.Id == entity.Id);
                if (dTO is null) continue;
                dTO.PaymentSystemControlTitle = entity.PaymentSystemControl.Translations.FirstOrDefault()?.Title;
            }
            return data;
        }
        public async Task<PaymentSystemControlServiceGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            PaymentSystemControlServiceGetDTO? data = entity is null ? null : _mapper.Map<PaymentSystemControlServiceGetDTO>(entity);

            if (data != null)
            {
                data.PaymentSystemControlTitle = entity?.PaymentSystemControl.Translations.FirstOrDefault()?.Title;
            }

            return data;
        }
        public async Task<bool> CreateAsync(PaymentSystemControlServiceCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<Service>(dto);
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");
                dto.Descriptions.TryGetValue(t.Key, out var description);

                return new PaymentSystemControlServiceTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Description = description,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, PaymentSystemControlServiceEditDTO dto)
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

                return new PaymentSystemControlServiceTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Description = description,
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
