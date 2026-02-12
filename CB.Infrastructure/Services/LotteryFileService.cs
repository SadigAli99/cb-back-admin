
using AutoMapper;
using CB.Application.DTOs.LotteryFile;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class LotteryFileService : ILotteryFileService
    {
        private readonly IGenericRepository<LotteryFile> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public LotteryFileService(
            IGenericRepository<LotteryFile> repository,
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

        public async Task<List<LotteryFileGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(x => x.Lottery)
                        .ThenInclude(x => x.Translations)
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<LotteryFileGetDTO> data = _mapper.Map<List<LotteryFileGetDTO>>(entities);

            foreach (var item in data)
            {
                var entity = entities.FirstOrDefault(x => x.Id == item.Id);
                item.LotteryTitle = entity?.Lottery?.Translations.FirstOrDefault()?.Title;
            }
            return data;
        }
        public async Task<LotteryFileGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);
            if (entity is null) return null;

            LotteryFileGetDTO? data = _mapper.Map<LotteryFileGetDTO>(entity);
            data.LotteryTitle = entity?.Lottery.Translations.FirstOrDefault()?.Title;
            return data;
        }
        public async Task<bool> CreateAsync(LotteryFileCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<LotteryFile>(dto);
            entity.File = await _fileService.UploadAsync(dto.File, "lotteries");
            entity.FileType = Path.GetExtension(dto.File.FileName)?.TrimStart('.');
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.CoverTitles.TryGetValue(t.Key, out var coverTitle);


                return new LotteryFileTranslation
                {
                    LanguageId = lang.Id,
                    CoverTitle = coverTitle,
                    Title = t.Value,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, LotteryFileEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            if (dto.File != null)
            {
                _fileService.Delete( entity.File ?? "");
                entity.File = await _fileService.UploadAsync(dto.File, "lotteries");
                entity.FileType = Path.GetExtension(dto.File.FileName)?.TrimStart('.');
            }

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);

                dto.CoverTitles.TryGetValue(t.Key, out var coverTitle);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                return new LotteryFileTranslation
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
            _fileService.Delete( entity.File ?? "");
            return await _repository.DeleteAsync(entity);
        }

    }
}
