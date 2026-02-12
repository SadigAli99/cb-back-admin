
using AutoMapper;
using CB.Application.DTOs.Director;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Extensions;
using CB.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class DirectorService : IDirectorService
    {
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IGenericRepository<Director> _repository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public DirectorService(
            IGenericRepository<Language> languageRepository,
            IGenericRepository<Director> repository,
            IFileService fileService,
            IMapper mapper
        )
        {
            _languageRepository = languageRepository;
            _fileService = fileService;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<DirectorGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<DirectorGetDTO> data = _mapper.Map<List<DirectorGetDTO>>(entities);
            return data;
        }
        public async Task<DirectorGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            DirectorGetDTO? data = entity is null ? null : _mapper.Map<DirectorGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(DirectorCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            dto.Slugs = SlugHelper.GenerateSlugs(dto.Fullnames);
            var entity = _mapper.Map<Director>(dto);
            entity.Image = await _fileService.UploadAsync(dto.File, "directors");
            entity.Translations = dto.Fullnames.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Positions.TryGetValue(t.Key, out var position);
                dto.Slugs.TryGetValue(t.Key, out var slug);

                return new DirectorTranslation
                {
                    LanguageId = lang.Id,
                    Fullname = t.Value,
                    Slug = slug,
                    Position = position ?? string.Empty,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, DirectorEditDTO dto)
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
                _fileService.Delete(entity.Image);
                entity.Image = await _fileService.UploadAsync(dto.File, "directors");
            }

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Fullnames.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Positions.TryGetValue(t.Key, out var position);
                dto.Slugs.TryGetValue(t.Key, out var slug);

                return new DirectorTranslation
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
            _fileService.Delete(entity.Image);
            return await _repository.DeleteAsync(entity);
        }

    }
}
