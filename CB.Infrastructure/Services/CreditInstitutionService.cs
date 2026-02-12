
using AutoMapper;
using CB.Application.DTOs.CreditInstitution;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class CreditInstitutionService : ICreditInstitutionService
    {
        private readonly IGenericRepository<CreditInstitution> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public CreditInstitutionService(
            IGenericRepository<CreditInstitution> repository,
            IGenericRepository<Language> languageRepository,
            IFileService fileService,
            IMapper mapper
        )
        {
            _languageRepository = languageRepository;
            _fileService = fileService;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CreditInstitutionGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(x => x.CreditInstitutionCategory!)
                        .ThenInclude(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .Include(x => x.CreditInstitutionSubCategory!)
                        .ThenInclude(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .ToListAsync();

            List<CreditInstitutionGetDTO> data = _mapper.Map<List<CreditInstitutionGetDTO>>(entities);

            foreach (var item in data)
            {
                var entity = entities.FirstOrDefault(x => x.Id == item.Id);
                if (entity is null) continue;
                item.CreditInstitutionCategory = entity.CreditInstitutionCategory?.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
                item.CreditInstitutionSubCategory = entity.CreditInstitutionSubCategory?.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            }
            return data;
        }
        public async Task<CreditInstitutionGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(x => x.CreditInstitutionCategory!)
                        .ThenInclude(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .Include(x => x.CreditInstitutionSubCategory!)
                        .ThenInclude(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            if (entity is null) return null;

            CreditInstitutionGetDTO? data = _mapper.Map<CreditInstitutionGetDTO>(entity);
            data.CreditInstitutionCategory = entity.CreditInstitutionCategory?.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            data.CreditInstitutionSubCategory = entity.CreditInstitutionSubCategory?.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;

            return data;
        }
        public async Task<bool> CreateAsync(CreditInstitutionCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<CreditInstitution>(dto);
            entity.File = await _fileService.UploadAsync(dto.File, "credit-institutions");
            entity.FileType = Path.GetExtension(dto.File.FileName)?.TrimStart('.');
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.CoverTitles.TryGetValue(t.Key, out var coverTitle);


                return new CreditInstitutionTranslation
                {
                    LanguageId = lang.Id,
                    CoverTitle = coverTitle,
                    Title = t.Value,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, CreditInstitutionEditDTO dto)
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
                entity.File = await _fileService.UploadAsync(dto.File, "credit-institutions");
                entity.FileType = Path.GetExtension(dto.File.FileName)?.TrimStart('.');
            }

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);

                dto.CoverTitles.TryGetValue(t.Key, out var coverTitle);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                return new CreditInstitutionTranslation
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
            _fileService.Delete(entity.File);
            return await _repository.DeleteAsync(entity);
        }

    }
}
