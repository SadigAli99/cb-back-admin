
using AutoMapper;
using CB.Application.DTOs.Office;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Extensions;
using CB.Shared.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class OfficeService : IOfficeService
    {
        private readonly IGenericRepository<Office> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public OfficeService(
            IGenericRepository<Office> repository,
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

        public async Task<List<OfficeGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<OfficeGetDTO> data = _mapper.Map<List<OfficeGetDTO>>(entities);
            return data;
        }
        public async Task<OfficeGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            OfficeGetDTO? data = entity is null ? null : _mapper.Map<OfficeGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(OfficeCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<Office>(dto);
            entity.Image = await dto.ImageFile.FileUpload(_env.WebRootPath, "offices");
            entity.Statute = await dto.StatuteFile.FileUpload(_env.WebRootPath, "offices");
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Chairmen.TryGetValue(t.Key, out var chairman);
                dto.Addresses.TryGetValue(t.Key, out var address);

                return new OfficeTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Chairman = chairman ?? string.Empty,
                    Address = address ?? string.Empty,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, OfficeEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            if (dto.ImageFile != null)
            {
                FileManager.FileDelete(_env.WebRootPath, entity.Image ?? "");
                entity.Image = await dto.ImageFile.FileUpload(_env.WebRootPath, "offices");
            }

            if (dto.StatuteFile != null)
            {
                FileManager.FileDelete(_env.WebRootPath, entity.Statute ?? "");
                entity.Statute = await dto.StatuteFile.FileUpload(_env.WebRootPath, "offices");
            }

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Addresses.TryGetValue(t.Key, out var address);
                dto.Chairmen.TryGetValue(t.Key, out var chairman);


                return new OfficeTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Address = address ?? string.Empty,
                    Chairman = chairman ?? string.Empty,
                };
            }).ToList();



            return await _repository.UpdateAsync(entity);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null) return false;
            FileManager.FileDelete(_env.WebRootPath, entity.Statute ?? "");
            FileManager.FileDelete(_env.WebRootPath, entity.Image ?? "");
            return await _repository.DeleteAsync(entity);
        }

    }
}
