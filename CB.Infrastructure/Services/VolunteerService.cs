
using AutoMapper;
using CB.Application.DTOs.Volunteer;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Extensions;
using CB.Shared.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class VolunteerService : IVolunteerService
    {
        private readonly IGenericRepository<Volunteer> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public VolunteerService(
            IGenericRepository<Volunteer> repository,
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

        public async Task<List<VolunteerGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<VolunteerGetDTO> data = _mapper.Map<List<VolunteerGetDTO>>(entities);
            return data;
        }
        public async Task<VolunteerGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            VolunteerGetDTO? data = entity is null ? null : _mapper.Map<VolunteerGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(VolunteerCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<Volunteer>(dto);
            entity.Image = await dto.File.FileUpload(_env.WebRootPath, "volunteers");
            entity.Translations = dto.Fullnames.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Descriptions.TryGetValue(t.Key, out var description);

                return new VolunteerTranslation
                {
                    LanguageId = lang.Id,
                    Fullname = t.Value,
                    Description = description ?? string.Empty,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, VolunteerEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            if (dto.File != null)
            {
                FileManager.FileDelete(_env.WebRootPath, entity.Image ?? "");
                entity.Image = await dto.File.FileUpload(_env.WebRootPath, "volunteers");
            }

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Fullnames.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Descriptions.TryGetValue(t.Key, out var description);

                return new VolunteerTranslation
                {
                    LanguageId = lang.Id,
                    Fullname = t.Value,
                    Description = description ?? string.Empty,
                };
            }).ToList();



            return await _repository.UpdateAsync(entity);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null) return false;
            FileManager.FileDelete(_env.WebRootPath, entity.Image ?? "");
            return await _repository.DeleteAsync(entity);
        }

    }
}
