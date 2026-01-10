using AutoMapper;
using CB.Application.DTOs.MonetaryPolicyDecision;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class MonetaryPolicyDecisionService : IMonetaryPolicyDecisionService
    {
        private readonly IGenericRepository<MonetaryPolicyDecision> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public MonetaryPolicyDecisionService(
            IGenericRepository<MonetaryPolicyDecision> repository,
            IGenericRepository<Language> languageRepository,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<List<MonetaryPolicyDecisionGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                .Include(t => t.Translations)
                .ThenInclude(tt => tt.Language)
                .ToListAsync();

            return _mapper.Map<List<MonetaryPolicyDecisionGetDTO>>(entities);
        }

        public async Task<MonetaryPolicyDecisionGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                .Include(t => t.Translations)
                .ThenInclude(tt => tt.Language)
                .FirstOrDefaultAsync(t => t.Id == id);

            return entity == null ? null : _mapper.Map<MonetaryPolicyDecisionGetDTO>(entity);
        }

        public async Task<bool> CreateAsync(MonetaryPolicyDecisionCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();



            var entity = new MonetaryPolicyDecision
            {
                Year = dto.Year,
                Translations = dto.Titles.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    dto.Slugs.TryGetValue(v.Key, out var slug);
                    dto.Descriptions.TryGetValue(v.Key, out var description);

                    return new MonetaryPolicyDecisionTranslation
                    {
                        LanguageId = lang.Id,
                        Title = v.Value,
                        Slug = slug ?? string.Empty,
                        Description = description ?? string.Empty
                    };
                }).ToList()
            };

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, MonetaryPolicyDecisionEditDTO dto)
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

                dto.Slugs.TryGetValue(v.Key, out var slug);
                dto.Descriptions.TryGetValue(v.Key, out var description);

                return new MonetaryPolicyDecisionTranslation
                {
                    LanguageId = lang.Id,
                    Title = v.Value,
                    Slug = slug ?? string.Empty,
                    Description = description ?? string.Empty
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
