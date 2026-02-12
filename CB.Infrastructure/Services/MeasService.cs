
using AutoMapper;
using CB.Application.DTOs.Meas;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class MeasService : IMeasService
    {
        private readonly IGenericRepository<Meas> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public MeasService(
            IGenericRepository<Meas> repository,
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

        public async Task<List<MeasGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(x => x.IssuerType)
                        .ThenInclude(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .Include(x => x.InformationType)
                        .ThenInclude(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .Include(x => x.SecurityType)
                        .ThenInclude(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<MeasGetDTO> data = _mapper.Map<List<MeasGetDTO>>(entities);
            return data;
        }
        public async Task<MeasGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(x => x.IssuerType)
                        .ThenInclude(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .Include(x => x.InformationType)
                        .ThenInclude(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .Include(x => x.SecurityType)
                        .ThenInclude(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            MeasGetDTO? data = entity is null ? null : _mapper.Map<MeasGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(MeasCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<Meas>(dto);
            entity.PdfFile = await _fileService.UploadAsync(dto.File, "meas");
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                return new MeasTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, MeasEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;


            _mapper.Map(dto, entity);

            if (dto.File != null)
            {
                _fileService.Delete( entity.PdfFile ?? "");
                entity.PdfFile = await _fileService.UploadAsync(dto.File, "meas");
            }

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                return new MeasTranslation
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
            _fileService.Delete( entity.PdfFile ?? "");
            return await _repository.DeleteAsync(entity);
        }

    }
}
