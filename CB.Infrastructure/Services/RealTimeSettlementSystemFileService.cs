
using AutoMapper;
using CB.Application.DTOs.RealTimeSettlementSystemFile;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class RealTimeSettlementSystemFileService : IRealTimeSettlementSystemFileService
    {
        private readonly IGenericRepository<RealTimeSettlementSystemFile> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public RealTimeSettlementSystemFileService(
            IGenericRepository<RealTimeSettlementSystemFile> repository,
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

        public async Task<List<RealTimeSettlementSystemFileGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(x => x.RealTimeSettlementSystem)
                        .ThenInclude(x => x.Translations)
                        .Include(b => b.Translations)
                        .ThenInclude(x => x.Language)
                        .ToListAsync();

            List<RealTimeSettlementSystemFileGetDTO> data = _mapper.Map<List<RealTimeSettlementSystemFileGetDTO>>(entities);

            foreach (var item in data)
            {
                var entity = entities.FirstOrDefault(x => x.Id == item.Id);
                item.RealTimeSettlementSystemTitle = entity?.RealTimeSettlementSystem?.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            }
            return data;
        }
        public async Task<RealTimeSettlementSystemFileGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(x => x.Language)
                        .Include(x => x.RealTimeSettlementSystem)
                        .ThenInclude(x => x.Translations)
                        .FirstOrDefaultAsync(b => b.Id == id);
            if (entity is null) return null;

            RealTimeSettlementSystemFileGetDTO? data = _mapper.Map<RealTimeSettlementSystemFileGetDTO>(entity);
            data.RealTimeSettlementSystemTitle = entity?.RealTimeSettlementSystem.Translations.FirstOrDefault()?.Title;
            return data;
        }
        public async Task<bool> CreateAsync(RealTimeSettlementSystemFileCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<RealTimeSettlementSystemFile>(dto);
            entity.File = await dto.File.FileUpload(_env.WebRootPath, "realtime-settlement-systems");
            entity.FileType = Path.GetExtension(dto.File.FileName)?.TrimStart('.');
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");


                return new RealTimeSettlementSystemFileTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, RealTimeSettlementSystemFileEditDTO dto)
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
                entity.File = await dto.File.FileUpload(_env.WebRootPath, "realtime-settlement-systems");
                entity.FileType = Path.GetExtension(dto.File.FileName)?.TrimStart('.');
            }

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);

                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                return new RealTimeSettlementSystemFileTranslation
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
            FileManager.FileDelete(_env.WebRootPath, entity.File ?? "");
            return await _repository.DeleteAsync(entity);
        }

    }
}
