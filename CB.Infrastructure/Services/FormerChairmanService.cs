
using AutoMapper;
using CB.Application.DTOs.FormerChairman;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class FormerChairmanService : IFormerChairmanService
    {
        private readonly IGenericRepository<FormerChairman> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public FormerChairmanService(
            IGenericRepository<FormerChairman> repository,
            IGenericRepository<Language> languageRepository,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<List<FormerChairmanGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<FormerChairmanGetDTO> data = _mapper.Map<List<FormerChairmanGetDTO>>(entities);
            return data;
        }
        public async Task<FormerChairmanGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            FormerChairmanGetDTO? data = entity is null ? null : _mapper.Map<FormerChairmanGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(FormerChairmanCreateDTO dto)
        {

            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<FormerChairman>(dto);
            entity.Translations = dto.Descriptions.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Fullnames.TryGetValue(t.Key, out var fullname);


                return new FormerChairmanTranslation
                {
                    LanguageId = lang.Id,
                    Description = t.Value,
                    Fullname = fullname ?? string.Empty,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, FormerChairmanEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Descriptions.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Fullnames.TryGetValue(t.Key, out var fullname);


                return new FormerChairmanTranslation
                {
                    LanguageId = lang.Id,
                    Description = t.Value,
                    Fullname = fullname ?? string.Empty,
                };
            }).ToList();

            return await _repository.UpdateAsync(entity);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null) return false;
            return await _repository.DeleteAsync(entity);
        }

    }
}
