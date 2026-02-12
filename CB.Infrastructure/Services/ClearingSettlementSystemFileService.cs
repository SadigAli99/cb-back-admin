
using AutoMapper;
using CB.Application.DTOs.ClearingSettlementSystemFile;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class ClearingSettlementSystemFileService : IClearingSettlementSystemFileService
    {
        private readonly IGenericRepository<ClearingSettlementSystemFile> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public ClearingSettlementSystemFileService(
            IGenericRepository<ClearingSettlementSystemFile> repository,
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

        public async Task<List<ClearingSettlementSystemFileGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(x => x.ClearingSettlementSystem)
                        .ThenInclude(x => x.Translations)
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<ClearingSettlementSystemFileGetDTO> data = _mapper.Map<List<ClearingSettlementSystemFileGetDTO>>(entities);

            foreach (var item in data)
            {
                var entity = entities.FirstOrDefault(x => x.Id == item.Id);
                item.ClearingSettlementSystemTitle = entity?.ClearingSettlementSystem?.Translations.FirstOrDefault()?.Title;
            }
            return data;
        }
        public async Task<ClearingSettlementSystemFileGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);
            if (entity is null) return null;

            ClearingSettlementSystemFileGetDTO? data = _mapper.Map<ClearingSettlementSystemFileGetDTO>(entity);
            data.ClearingSettlementSystemTitle = entity?.ClearingSettlementSystem.Translations.FirstOrDefault()?.Title;
            return data;
        }
        public async Task<bool> CreateAsync(ClearingSettlementSystemFileCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<ClearingSettlementSystemFile>(dto);
            entity.File = await _fileService.UploadAsync(dto.File, "clearing-settlement-systems");
            entity.FileType = Path.GetExtension(dto.File.FileName)?.TrimStart('.');
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");


                return new ClearingSettlementSystemFileTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, ClearingSettlementSystemFileEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            if (dto.File != null)
            {
                _fileService.Delete(entity.File);
                entity.File = await _fileService.UploadAsync(dto.File, "clearing-settlement-systems");
                entity.FileType = Path.GetExtension(dto.File.FileName)?.TrimStart('.');
            }

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);

                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                return new ClearingSettlementSystemFileTranslation
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
            _fileService.Delete(entity.File);
            return await _repository.DeleteAsync(entity);
        }

    }
}
