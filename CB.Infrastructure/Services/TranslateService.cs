using AutoMapper;
using CB.Application.DTOs.Translate;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class TranslateService : ITranslateService
    {
        private readonly IGenericRepository<Translate> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public TranslateService(
            IGenericRepository<Translate> repository,
            IGenericRepository<Language> languageRepository,
             IMapper mapper
             )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<List<TranslateGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                .Include(t => t.Translations)
                .ThenInclude(tt => tt.Language)
                .ToListAsync();

            return _mapper.Map<List<TranslateGetDTO>>(entities);
        }

        public async Task<TranslateGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                .Include(t => t.Translations)
                .ThenInclude(tt => tt.Language)
                .FirstOrDefaultAsync(t => t.Id == id);

            return entity == null ? null : _mapper.Map<TranslateGetDTO>(entity);
        }

        public async Task<bool> CreateAsync(TranslateCreateDTO dto)
        {
            bool exists = await _repository.GetQuery().AnyAsync(x => x.Key.ToLower() == dto.Key.ToLower());
            if (exists)
                throw new Exception($"'{dto.Key}' açarı artıq mövcuddur.");

            var languages = await _languageRepository.GetAllAsync();

            var entity = new Translate
            {
                Key = dto.Key,
                Translations = dto.Values.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    return new TranslateTranslation
                    {
                        LanguageId = lang.Id,
                        Value = v.Value
                    };
                }).ToList()
            };

            return await _repository.AddAsync(entity);
        }


        public async Task<bool> UpdateAsync(int id, TranslateEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                .Include(t => t.Translations)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (entity == null) return false;

            entity.Key = dto.Key;

            var languages = await _languageRepository.GetQuery().ToListAsync();

            entity.Translations.Clear();
            entity.Translations = dto.Values.Select(v =>
            {
                var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                return new TranslateTranslation
                {
                    LanguageId = lang?.Id ?? 0,
                    Value = v.Value
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

        public async Task<string> GetValueAsync(string key, string lang)
        {
            var entity = await _repository.GetQuery()
                        .Include(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .FirstOrDefaultAsync(x => x.Key == key);

            if (entity is null)
                return key;

            string value = entity.Translations.FirstOrDefault(tt => tt.Language.Code == key)?.Value ?? "";

            return !string.IsNullOrEmpty(value) ? value : key;
        }
    }
}
