
using AutoMapper;
using CB.Application.DTOs.PaperMoney;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class PaperMoneyService : IPaperMoneyService
    {
        private readonly IGenericRepository<Money> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public PaperMoneyService(
            IGenericRepository<Money> repository,
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

        public async Task<List<PaperMoneyGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<PaperMoneyGetDTO> data = _mapper.Map<List<PaperMoneyGetDTO>>(entities);
            return data;
        }
        public async Task<PaperMoneyGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            PaperMoneyGetDTO? data = entity is null ? null : _mapper.Map<PaperMoneyGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(PaperMoneyCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<Money>(dto);
            entity.Image = await _fileService.UploadAsync(dto.File, "moneys");
            entity.Type = MoneyType.PAPER;
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Topics.TryGetValue(t.Key, out var topic);
                dto.ReleaseDates.TryGetValue(t.Key, out var releaseDate);


                return new MoneyTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Topic = topic,
                    ReleaseDate = releaseDate,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, PaperMoneyEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            if (dto.File != null)
            {
                _fileService.Delete( entity.Image ?? "");
                entity.Image = await _fileService.UploadAsync(dto.File, "moneys");
            }

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);

                dto.Topics.TryGetValue(t.Key, out var topic);
                dto.ReleaseDates.TryGetValue(t.Key, out var releaseDate);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                return new MoneyTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Topic = topic,
                    ReleaseDate = releaseDate,
                };
            }).ToList();



            return await _repository.UpdateAsync(entity);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null) return false;
            _fileService.Delete( entity.Image ?? "");
            return await _repository.DeleteAsync(entity);
        }

    }
}
