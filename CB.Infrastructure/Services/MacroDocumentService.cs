
using AutoMapper;
using CB.Application.DTOs.MacroDocument;
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
    public class MacroDocumentService : IMacroDocumentService
    {
        private readonly IGenericRepository<MacroDocument> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public MacroDocumentService(
            IGenericRepository<MacroDocument> repository,
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

        public async Task<List<MacroDocumentGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<MacroDocumentGetDTO> data = _mapper.Map<List<MacroDocumentGetDTO>>(entities);
            return data;
        }
        public async Task<MacroDocumentGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            MacroDocumentGetDTO? data = entity is null ? null : _mapper.Map<MacroDocumentGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(MacroDocumentCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<MacroDocument>(dto);
            entity.Icon = await dto.File.FileUpload(_env.WebRootPath, "macrodocuments");
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Texts.TryGetValue(t.Key, out var text);

                return new MacroDocumentTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Text = text ?? string.Empty,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, MacroDocumentEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            if (dto.File != null)
            {
                FileManager.FileDelete(_env.WebRootPath, entity.Icon ?? "");
                entity.Icon = await dto.File.FileUpload(_env.WebRootPath, "macrodocuments");
            }

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Texts.TryGetValue(t.Key, out var text);

                return new MacroDocumentTranslation
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
            FileManager.FileDelete(_env.WebRootPath, entity.Icon ?? "");
            return await _repository.DeleteAsync(entity);
        }

    }
}
