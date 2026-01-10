
using AutoMapper;
using CB.Application.DTOs.NakhchivanPublication;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class NakhchivanPublicationService : INakhchivanPublicationService
    {
        private readonly IGenericRepository<NakhchivanPublication> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public NakhchivanPublicationService(
            IGenericRepository<NakhchivanPublication> repository,
            IGenericRepository<Language> languageRepository,
            IWebHostEnvironment env,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
            _env = env;
        }

        public async Task<List<NakhchivanPublicationGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<NakhchivanPublicationGetDTO> data = _mapper.Map<List<NakhchivanPublicationGetDTO>>(entities);
            return data;
        }
        public async Task<NakhchivanPublicationGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            NakhchivanPublicationGetDTO? data = entity is null ? null : _mapper.Map<NakhchivanPublicationGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(NakhchivanPublicationCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<NakhchivanPublication>(dto);
            entity.File = await dto.File.FileUpload(_env.WebRootPath, "interviews");
            entity.FileType = Path.GetExtension(dto.File.FileName)?.TrimStart('.');
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.CoverTitles.TryGetValue(t.Key, out var coverTitle);

                return new NakhchivanPublicationTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    CoverTitle = coverTitle,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, NakhchivanPublicationEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            if (dto.File != null)
            {
                FileManager.FileDelete(_env.WebRootPath, entity.File ?? "");
                entity.File = await dto.File.FileUpload(_env.WebRootPath, "interviews");
                entity.FileType = Path.GetExtension(dto.File.FileName)?.TrimStart('.');
            }

            var languages = await _languageRepository.GetAllAsync();


            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.CoverTitles.TryGetValue(t.Key, out var coverTitle);

                return new NakhchivanPublicationTranslation
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
            FileManager.FileDelete(_env.WebRootPath, entity.File ?? "");
            return await _repository.DeleteAsync(entity);
        }

    }
}
