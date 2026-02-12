
using AutoMapper;
using CB.Application.DTOs.ComplaintIndex;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class ComplaintIndexService : IComplaintIndexService
    {
        private readonly IGenericRepository<ComplaintIndex> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public ComplaintIndexService(
            IGenericRepository<ComplaintIndex> repository,
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

        public async Task<List<ComplaintIndexGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(x => x.ComplaintIndexCategory)
                        .ThenInclude(x => x.Translations)
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<ComplaintIndexGetDTO> data = _mapper.Map<List<ComplaintIndexGetDTO>>(entities);

            foreach (var item in data)
            {
                var entity = entities.FirstOrDefault(x => x.Id == item.Id);
                item.ComplaintIndexCategoryTitle = entity?.ComplaintIndexCategory?.Translations.FirstOrDefault()?.Title;
            }
            return data;
        }
        public async Task<ComplaintIndexGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);
            if (entity is null) return null;

            ComplaintIndexGetDTO? data = _mapper.Map<ComplaintIndexGetDTO>(entity);
            data.ComplaintIndexCategoryTitle = entity?.ComplaintIndexCategory.Translations.FirstOrDefault()?.Title;
            return data;
        }
        public async Task<bool> CreateAsync(ComplaintIndexCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<ComplaintIndex>(dto);
            entity.File = await _fileService.UploadAsync(dto.File, "complaint-indexes");
            entity.FileType = Path.GetExtension(dto.File.FileName)?.TrimStart('.');
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.CoverTitles.TryGetValue(t.Key, out var coverTitle);


                return new ComplaintIndexTranslation
                {
                    LanguageId = lang.Id,
                    CoverTitle = coverTitle,
                    Title = t.Value,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, ComplaintIndexEditDTO dto)
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
                entity.File = await _fileService.UploadAsync(dto.File, "complaint-indexes");
                entity.FileType = Path.GetExtension(dto.File.FileName)?.TrimStart('.');
            }

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);

                dto.CoverTitles.TryGetValue(t.Key, out var coverTitle);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                return new ComplaintIndexTranslation
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
