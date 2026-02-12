
using AutoMapper;
using CB.Application.DTOs.AnnualReport;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class AnnualReportService : IAnnualReportService
    {
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IGenericRepository<AnnualReport> _repository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public AnnualReportService(
            IGenericRepository<Language> languageRepository,
            IGenericRepository<AnnualReport> repository,
            IFileService fileService,
            IMapper mapper
        )
        {
            _languageRepository = languageRepository;
            _fileService = fileService;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<AnnualReportGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<AnnualReportGetDTO> data = _mapper.Map<List<AnnualReportGetDTO>>(entities);
            return data;
        }
        public async Task<AnnualReportGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            AnnualReportGetDTO? data = entity is null ? null : _mapper.Map<AnnualReportGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(AnnualReportCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<AnnualReport>(dto);
            entity.File = await _fileService.UploadAsync(dto.File, "annual-reports");
            entity.FileType = Path.GetExtension(dto.File.FileName)?.TrimStart('.');
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.CoverTitles.TryGetValue(t.Key, out var coverTitle);

                return new AnnualReportTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    CoverTitle = coverTitle,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, AnnualReportEditDTO dto)
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
                entity.File = await _fileService.UploadAsync(dto.File, "annual-reports");
                entity.FileType = Path.GetExtension(dto.File.FileName)?.TrimStart('.');
            }

            var languages = await _languageRepository.GetAllAsync();


            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.CoverTitles.TryGetValue(t.Key, out var coverTitle);

                return new AnnualReportTranslation
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
            _fileService.Delete(entity.File);
            return await _repository.DeleteAsync(entity);
        }

    }
}
