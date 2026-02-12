
using AutoMapper;
using CB.Application.DTOs.OutOfCirculationCategory;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class OutOfCirculationCategoryService : IOutOfCirculationCategoryService
    {
        private readonly IGenericRepository<OutOfCirculationCategory> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public OutOfCirculationCategoryService(
            IGenericRepository<OutOfCirculationCategory> repository,
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

        public async Task<List<OutOfCirculationCategoryGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<OutOfCirculationCategoryGetDTO> data = _mapper.Map<List<OutOfCirculationCategoryGetDTO>>(entities);
            return data;
        }
        public async Task<OutOfCirculationCategoryGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            OutOfCirculationCategoryGetDTO? data = entity is null ? null : _mapper.Map<OutOfCirculationCategoryGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(OutOfCirculationCategoryCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<OutOfCirculationCategory>(dto);
            entity.Image = await _fileService.UploadAsync(dto.File, "out-of-circulations");
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");




                return new OutOfCirculationCategoryTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,

                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, OutOfCirculationCategoryEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            if (dto.File != null)
            {
                _fileService.Delete( entity.Image ?? "");
                entity.Image = await _fileService.UploadAsync(dto.File, "out-of-circulations");
            }

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);


                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                return new OutOfCirculationCategoryTranslation
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
            _fileService.Delete( entity.Image ?? "");
            return await _repository.DeleteAsync(entity);
        }

    }
}
