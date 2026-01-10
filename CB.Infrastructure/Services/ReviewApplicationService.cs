using AutoMapper;
using CB.Application.DTOs.ReviewApplication;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class ReviewApplicationService : IReviewApplicationService
    {
        private readonly IGenericRepository<ReviewApplication> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public ReviewApplicationService(
            IGenericRepository<ReviewApplication> repository,
            IGenericRepository<Language> languageRepository,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<List<ReviewApplicationGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                .Include(t => t.Translations)
                .ThenInclude(tt => tt.Language)
                .ToListAsync();

            return _mapper.Map<List<ReviewApplicationGetDTO>>(entities);
        }

        public async Task<ReviewApplicationGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                .Include(t => t.Translations)
                .ThenInclude(tt => tt.Language)
                .FirstOrDefaultAsync(t => t.Id == id);

            return entity == null ? null : _mapper.Map<ReviewApplicationGetDTO>(entity);
        }

        public async Task<bool> CreateAsync(ReviewApplicationCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();

            var entity = new ReviewApplication
            {
                Translations = dto.Titles.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    dto.Descriptions.TryGetValue(v.Key, out var description);

                    return new ReviewApplicationTranslation
                    {
                        LanguageId = lang.Id,
                        Title = v.Value,
                        Description = description,
                    };
                }).ToList()
            };

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, ReviewApplicationEditDTO dto)
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

                dto.Descriptions.TryGetValue(v.Key, out var description);

                return new ReviewApplicationTranslation
                {
                    LanguageId = lang.Id,
                    Title = v.Value,
                    Description = description
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
