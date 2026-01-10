using AutoMapper;
using CB.Application.DTOs.BankNoteCategory;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class BankNoteCategoryService : IBankNoteCategoryService
    {
        private readonly IGenericRepository<BankNoteCategory> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public BankNoteCategoryService(
            IGenericRepository<BankNoteCategory> repository,
            IGenericRepository<Language> languageRepository,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<List<BankNoteCategoryGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                .Include(t => t.Translations)
                .ThenInclude(tt => tt.Language)
                .ToListAsync();

            return _mapper.Map<List<BankNoteCategoryGetDTO>>(entities);
        }

        public async Task<BankNoteCategoryGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                .Include(t => t.Translations)
                .ThenInclude(tt => tt.Language)
                .FirstOrDefaultAsync(t => t.Id == id);

            return entity == null ? null : _mapper.Map<BankNoteCategoryGetDTO>(entity);
        }

        public async Task<bool> CreateAsync(BankNoteCategoryCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();

            var entity = new BankNoteCategory
            {
                Translations = dto.Titles.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    dto.Notes.TryGetValue(v.Key, out var note);
                    dto.Slugs.TryGetValue(v.Key, out var slug);
                    dto.ShortTitles.TryGetValue(v.Key, out var shortTitle);

                    return new BankNoteCategoryTranslation
                    {
                        LanguageId = lang.Id,
                        Title = v.Value,
                        Note = note ?? string.Empty,
                        Slug = slug ?? string.Empty,
                        ShortTitle = shortTitle ?? string.Empty
                    };
                }).ToList()
            };

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, BankNoteCategoryEditDTO dto)
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
                dto.ShortTitles.TryGetValue(v.Key, out var shortTitle);

                return new BankNoteCategoryTranslation
                {
                    LanguageId = lang.Id,
                    Title = v.Value,
                    Note = note ?? string.Empty,
                    Slug = slug ?? string.Empty,
                    ShortTitle = shortTitle ?? string.Empty
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
