using AutoMapper;
using CB.Application.DTOs.ControlMeasureCategory;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class ControlMeasureCategoryService : IControlMeasureCategoryService
    {
        private readonly IGenericRepository<ControlMeasureCategory> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public ControlMeasureCategoryService(
            IGenericRepository<ControlMeasureCategory> repository,
            IGenericRepository<Language> languageRepository,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<List<ControlMeasureCategoryGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                .Include(t => t.Translations)
                .ThenInclude(tt => tt.Language)
                .ToListAsync();

            return _mapper.Map<List<ControlMeasureCategoryGetDTO>>(entities);
        }

        public async Task<ControlMeasureCategoryGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                .Include(t => t.Translations)
                .ThenInclude(tt => tt.Language)
                .FirstOrDefaultAsync(t => t.Id == id);

            return entity == null ? null : _mapper.Map<ControlMeasureCategoryGetDTO>(entity);
        }

        public async Task<bool> CreateAsync(ControlMeasureCategoryCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();

            var entity = new ControlMeasureCategory
            {
                Translations = dto.Titles.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    return new ControlMeasureCategoryTranslation
                    {
                        LanguageId = lang.Id,
                        Title = v.Value
                    };
                }).ToList()
            };

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, ControlMeasureCategoryEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                .Include(t => t.Translations)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (entity == null) return false;

            _mapper.Map(dto, entity);

            var languages = await _languageRepository.GetQuery().ToListAsync();

            entity.Translations = dto.Titles.Select(v =>
            {
                var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                if (lang == null)
                    throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                return new ControlMeasureCategoryTranslation
                {
                    LanguageId = lang.Id,
                    Title = v.Value
                };
            }).ToList();

            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetQuery()
                .Include(t => t.Translations)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (entity == null) return false;

            return await _repository.DeleteAsync(entity);
        }
    }
}
