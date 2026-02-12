
using AutoMapper;
using CB.Application.DTOs.Mission;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class MissionService : IMissionService
    {
        private readonly IGenericRepository<Mission> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public MissionService(
            IGenericRepository<Mission> repository,
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

        public async Task<List<MissionGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<MissionGetDTO> data = _mapper.Map<List<MissionGetDTO>>(entities);
            return data;
        }
        public async Task<MissionGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            MissionGetDTO? data = entity is null ? null : _mapper.Map<MissionGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(MissionCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<Mission>(dto);
            entity.Icon = await _fileService.UploadAsync(dto.File, "macrodocuments");
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Texts.TryGetValue(t.Key, out var text);

                return new MissionTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Text = text ?? string.Empty,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, MissionEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            if (dto.File != null)
            {
                _fileService.Delete( entity.Icon ?? "");
                entity.Icon = await _fileService.UploadAsync(dto.File, "macrodocuments");
            }

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Texts.TryGetValue(t.Key, out var text);

                return new MissionTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Text = text ?? string.Empty,
                };
            }).ToList();



            return await _repository.UpdateAsync(entity);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null) return false;
            _fileService.Delete( entity.Icon ?? "");
            return await _repository.DeleteAsync(entity);
        }

    }
}
