
using AutoMapper;
using CB.Application.DTOs.LegalBasis;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CB.Infrastructure.Services
{
    public class LegalBasisService : ILegalBasisService
    {
        private readonly IGenericRepository<LegalBasis> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

        public LegalBasisService(
            IGenericRepository<Language> languageRepository,
            IGenericRepository<LegalBasis> repository,
            IWebHostEnvironment env,
            IConfiguration config,
            IMapper mapper
        )
        {
            _languageRepository = languageRepository;
            _repository = repository;
            _config = config;
            _mapper = mapper;
            _env = env;
        }

        public async Task<List<LegalBasisGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<LegalBasisGetDTO> data = _mapper.Map<List<LegalBasisGetDTO>>(entities);
            foreach (LegalBasisGetDTO item in data)
            {
                var entity = entities.FirstOrDefault(x => x.Id == item.Id);
                item.File = $"{_config["BaseUrl"]}{entity?.File}";
            }
            return data;
        }
        public async Task<LegalBasisGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            if (entity is null) return null;

            LegalBasisGetDTO? data = _mapper.Map<LegalBasisGetDTO>(entity);
            data.File = $"{_config["BaseUrl"]}{entity.File}";

            return data;
        }
        public async Task<bool> CreateAsync(LegalBasisCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<LegalBasis>(dto);
            entity.File = await dto.File.FileUpload(_env.WebRootPath, "legal-bases");
            entity.FileType = Path.GetExtension(dto.File.FileName)?.TrimStart('.');
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");



                return new LegalBasisTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, LegalBasisEditDTO dto)
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
                entity.File = await dto.File.FileUpload(_env.WebRootPath, "legal-bases");
                entity.FileType = Path.GetExtension(dto.File.FileName)?.TrimStart('.');
            }

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                return new LegalBasisTranslation
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
