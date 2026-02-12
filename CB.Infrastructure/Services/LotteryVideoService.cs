
using AutoMapper;
using CB.Application.DTOs.LotteryVideo;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class LotteryVideoService : ILotteryVideoService
    {
        private readonly IGenericRepository<LotteryVideo> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public LotteryVideoService(
            IGenericRepository<LotteryVideo> repository,
            IGenericRepository<Language> languageRepository,
            IFileService fileService,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<List<LotteryVideoGetDTO>> GetAllAsync(int? id)
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


            List<LotteryVideoGetDTO> data = _mapper.Map<List<LotteryVideoGetDTO>>(entities);

            foreach (var entity in entities)
            {
                LotteryVideoGetDTO? dTO = data.FirstOrDefault(x => x.Id == entity.Id);
                if (dTO is null) continue;
                dTO.LotteryTitle = entity.Lottery.Translations.FirstOrDefault()?.Title;
            }
            return data;
        }
        public async Task<LotteryVideoGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(x=>x.Lottery)
                        .ThenInclude(x=>x.Translations)
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            LotteryVideoGetDTO? data = entity is null ? null : _mapper.Map<LotteryVideoGetDTO>(entity);

            if (data != null)
            {
                data.LotteryTitle = entity?.Lottery.Translations.FirstOrDefault()?.Title;
            }

            return data;
        }
        public async Task<bool> CreateAsync(LotteryVideoCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<LotteryVideo>(dto);
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                return new LotteryVideoTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, LotteryVideoEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(x=>x.Lottery)
                        .ThenInclude(x=>x.Translations)
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


                return new LotteryVideoTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
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
