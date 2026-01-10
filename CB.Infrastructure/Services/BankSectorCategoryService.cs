using AutoMapper;
using CB.Application.DTOs.BankSectorCategory;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class BankSectorCategoryService : IBankSectorCategoryService
    {
        private readonly IGenericRepository<BankSectorCategory> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public BankSectorCategoryService(
            IGenericRepository<BankSectorCategory> repository,
            IGenericRepository<Language> languageRepository,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<List<BankSectorCategoryGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                .Include(t => t.Translations)
                .ThenInclude(tt => tt.Language)
                .ToListAsync();

            return _mapper.Map<List<BankSectorCategoryGetDTO>>(entities);
        }

        public async Task<BankSectorCategoryGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                .Include(t => t.Translations)
                .ThenInclude(tt => tt.Language)
                .FirstOrDefaultAsync(t => t.Id == id);

            return entity == null ? null : _mapper.Map<BankSectorCategoryGetDTO>(entity);
        }

        public async Task<bool> CreateAsync(BankSectorCategoryCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();

            var entity = new BankSectorCategory
            {
                Translations = dto.Titles.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    dto.Notes.TryGetValue(v.Key, out var note);
                    dto.Slugs.TryGetValue(v.Key, out var slug);

                    return new BankSectorCategoryTranslation
                    {
                        LanguageId = lang.Id,
                        Title = v.Value,
                        Note = note ?? string.Empty,
                        Slug = slug ?? string.Empty,
                    };
                }).ToList()
            };

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, BankSectorCategoryEditDTO dto)
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

                dto.Notes.TryGetValue(v.Key, out var note);
                dto.Slugs.TryGetValue(v.Key, out var slug);

                return new BankSectorCategoryTranslation
                {
                    LanguageId = lang.Id,
                    Title = v.Value,
                    Note = note ?? string.Empty,
                    Slug = slug ?? string.Empty,
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
