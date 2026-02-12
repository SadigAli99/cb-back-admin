
using AutoMapper;
using CB.Application.DTOs.Manager;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class ManagerService : IManagerService
    {
        private readonly IGenericRepository<Manager> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public ManagerService(
            IGenericRepository<Manager> repository,
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

        public async Task<List<ManagerGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<ManagerGetDTO> data = _mapper.Map<List<ManagerGetDTO>>(entities);
            return data;
        }
        public async Task<ManagerGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            ManagerGetDTO? data = entity is null ? null : _mapper.Map<ManagerGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(ManagerCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            dto.Slugs = SlugHelper.GenerateSlugs(dto.Fullnames);
            var entity = _mapper.Map<Manager>(dto);
            entity.Image = await _fileService.UploadAsync(dto.File, "managers");
            entity.Translations = dto.Fullnames.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Positions.TryGetValue(t.Key, out var position);
                dto.Slugs.TryGetValue(t.Key, out var slug);

                return new ManagerTranslation
                {
                    LanguageId = lang.Id,
                    Fullname = t.Value,
                    Slug = slug,
                    Position = position ?? string.Empty,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, ManagerEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            dto.Slugs = SlugHelper.GenerateSlugs(dto.Fullnames);

            _mapper.Map(dto, entity);

            if (dto.File != null)
            {
                _fileService.Delete( entity.Image ?? "");
                entity.Image = await _fileService.UploadAsync(dto.File, "managers");
            }

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Fullnames.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Positions.TryGetValue(t.Key, out var position);
                dto.Slugs.TryGetValue(t.Key, out var slug);

                return new ManagerTranslation
                {
                    LanguageId = lang.Id,
                    Fullname = t.Value,
                    Slug = slug,
                    Position = position ?? string.Empty,
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
