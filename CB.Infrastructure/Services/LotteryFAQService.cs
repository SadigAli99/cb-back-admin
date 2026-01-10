
using AutoMapper;
using CB.Application.DTOs.LotteryFAQ;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class LotteryFAQService : ILotteryFAQService
    {
        private readonly IGenericRepository<LotteryFAQ> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public LotteryFAQService(
            IGenericRepository<LotteryFAQ> repository,
            IGenericRepository<Language> languageRepository,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<List<LotteryFAQGetDTO>> GetAllAsync(int? id)
        {
            var query = _repository.GetQuery();

            if (id != default(int)) query = query.Where(x => x.LotteryId == id);

            var entities = await query
                        .Include(x => x.Lottery)
                        .ThenInclude(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();


            List<LotteryFAQGetDTO> data = _mapper.Map<List<LotteryFAQGetDTO>>(entities);

            foreach (var entity in entities)
            {
                LotteryFAQGetDTO? dTO = data.FirstOrDefault(x => x.Id == entity.Id);
                if (dTO is null) continue;
                dTO.LotteryTitle = entity.Lottery.Translations.FirstOrDefault()?.Title;
            }
            return data;
        }
        public async Task<LotteryFAQGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            LotteryFAQGetDTO? data = entity is null ? null : _mapper.Map<LotteryFAQGetDTO>(entity);

            if (data != null)
            {
                data.LotteryTitle = entity?.Lottery.Translations.FirstOrDefault()?.Title;
            }

            return data;
        }
        public async Task<bool> CreateAsync(LotteryFAQCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<LotteryFAQ>(dto);
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");
                dto.Descriptions.TryGetValue(t.Key, out var description);

                return new LotteryFAQTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Description = description,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, LotteryFAQEditDTO dto)
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

                return new LotteryFAQTranslation
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
