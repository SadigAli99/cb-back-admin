
using AutoMapper;
using CB.Application.DTOs.NakhchivanBulletin;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class NakhchivanBulletinService : INakhchivanBulletinService
    {
        private readonly IGenericRepository<NakhchivanBulletin> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public NakhchivanBulletinService(
            IGenericRepository<NakhchivanBulletin> repository,
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

        public async Task<List<NakhchivanBulletinGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<NakhchivanBulletinGetDTO> data = _mapper.Map<List<NakhchivanBulletinGetDTO>>(entities);
            return data;
        }
        public async Task<NakhchivanBulletinGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            NakhchivanBulletinGetDTO? data = entity is null ? null : _mapper.Map<NakhchivanBulletinGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(NakhchivanBulletinCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<NakhchivanBulletin>(dto);
            entity.File = await _fileService.UploadAsync(dto.File, "nakhchivan-bulletins");
            entity.FileType = Path.GetExtension(dto.File.FileName)?.TrimStart('.');
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.CoverTitles.TryGetValue(t.Key, out var coverTitle);

                return new NakhchivanBulletinTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    CoverTitle = coverTitle,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, NakhchivanBulletinEditDTO dto)
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
                entity.File = await _fileService.UploadAsync(dto.File, "nakhchivan-bulletins");
                entity.FileType = Path.GetExtension(dto.File.FileName)?.TrimStart('.');
            }

            var languages = await _languageRepository.GetAllAsync();


            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.CoverTitles.TryGetValue(t.Key, out var coverTitle);

                return new NakhchivanBulletinTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    CoverTitle = coverTitle,
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
