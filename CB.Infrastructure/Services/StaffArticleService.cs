using AutoMapper;
using CB.Application.DTOs.StaffArticle;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class StaffArticleService : IStaffArticleService
    {
        private readonly IGenericRepository<StaffArticle> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public StaffArticleService(
            IGenericRepository<StaffArticle> repository,
            IGenericRepository<Language> languageRepository,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<List<StaffArticleGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                .Include(t => t.Translations)
                .ThenInclude(tt => tt.Language)
                .ToListAsync();

            return _mapper.Map<List<StaffArticleGetDTO>>(entities);
        }

        public async Task<StaffArticleGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                .Include(t => t.Translations)
                .ThenInclude(tt => tt.Language)
                .FirstOrDefaultAsync(t => t.Id == id);

            return entity == null ? null : _mapper.Map<StaffArticleGetDTO>(entity);
        }

        public async Task<bool> CreateAsync(StaffArticleCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();

            dto.Slugs = SlugHelper.GenerateSlugs(dto.Titles);

            var entity = new StaffArticle
            {
                Year = dto.Year,
                Translations = dto.Titles.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    dto.Descriptions.TryGetValue(v.Key, out var description);
                    dto.SubTitles.TryGetValue(v.Key, out var subTitle);
                    dto.Slugs.TryGetValue(v.Key, out var slug);

                    return new StaffArticleTranslation
                    {
                        LanguageId = lang.Id,
                        Title = v.Value,
                        Description = description ?? string.Empty,
                        SubTitle = subTitle,
                        Slug = slug
                    };
                }).ToList()
            };

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, StaffArticleEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                .Include(t => t.Translations)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (entity == null) return false;

            _mapper.Map(dto, entity);

            var languages = await _languageRepository.GetQuery().ToListAsync();

            dto.Slugs = SlugHelper.GenerateSlugs(dto.Titles);

            entity.Translations = dto.Titles.Select(v =>
            {
                var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                if (lang == null)
                    throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                dto.Descriptions.TryGetValue(v.Key, out var description);
                dto.SubTitles.TryGetValue(v.Key, out var subTitle);
                dto.Slugs.TryGetValue(v.Key, out var slug);

                return new StaffArticleTranslation
                {
                    LanguageId = lang.Id,
                    Title = v.Value,
                    Description = description ?? string.Empty,
                    SubTitle = subTitle,
                    Slug = slug
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
