
using AutoMapper;
using CB.Application.DTOs.InformationMemorandum;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class InformationMemorandumService : IInformationMemorandumService
    {
        private readonly IGenericRepository<InformationMemorandum> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public InformationMemorandumService(
            IGenericRepository<InformationMemorandum> repository,
            IGenericRepository<Language> languageRepository,
            IFileService fileService,
            IMapper mapper
        )
        {
            _languageRepository = languageRepository;
            _fileService = fileService;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<InformationMemorandumGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<InformationMemorandumGetDTO> data = _mapper.Map<List<InformationMemorandumGetDTO>>(entities);
            return data;
        }
        public async Task<InformationMemorandumGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            InformationMemorandumGetDTO? data = entity is null ? null : _mapper.Map<InformationMemorandumGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(InformationMemorandumCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<InformationMemorandum>(dto);
            // entity.File = await _fileService.UploadAsync(dto.File, "information-memorandums");
            entity.File = await _fileService.UploadAsync(dto.File, "information-memorandums");
            entity.FileType = Path.GetExtension(dto.File.FileName)?.TrimStart('.');
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.CoverTitles.TryGetValue(t.Key, out var coverTitle);


                return new InformationMemorandumTranslation
                {
                    LanguageId = lang.Id,
                    CoverTitle = coverTitle,
                    Title = t.Value,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, InformationMemorandumEditDTO dto)
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
                entity.File = await _fileService.UploadAsync(dto.File, "information-memorandums");
                entity.FileType = (Path.GetExtension(dto.File.FileName) ?? "").TrimStart('.');
            }

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);

                dto.CoverTitles.TryGetValue(t.Key, out var coverTitle);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                return new InformationMemorandumTranslation
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
