using AutoMapper;
using CB.Application.DTOs.TrainingJournalist;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class TrainingJournalistService : ITrainingJournalistService
    {
        private readonly IGenericRepository<TrainingJournalist> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public TrainingJournalistService(
            IMapper mapper,
            IGenericRepository<TrainingJournalist> repository,
            IGenericRepository<Language> languageRepository,
            IWebHostEnvironment env
        )
        {
            _env = env;
            _mapper = mapper;
            _repository = repository;
            _languageRepository = languageRepository;
        }

        public async Task<bool> CreateOrUpdate(TrainingJournalistPostDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            TrainingJournalist? entity = await _repository.GetQuery()
                                        .Include(h => h.Translations)
                                        .ThenInclude(ht => ht.Language)
                                        .FirstOrDefaultAsync();

            bool result;

            if (entity is null)
            {
                entity = _mapper.Map<TrainingJournalist>(dto);

                foreach (IFormFile file in dto.Files)
                {
                    entity.Images.Add(new TrainingJournalistImage
                    {
                        Image = await file.FileUpload(_env.WebRootPath, "training-journalists")
                    });
                }

                entity.Translations = dto.Descriptions.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);

                    dto.Titles.TryGetValue(v.Key, out var title);

                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    return new TrainingJournalistTranslation
                    {
                        LanguageId = lang.Id,
                        Description = v.Value,
                        Title = title,
                    };
                }).ToList();

                result = await _repository.AddAsync(entity);
            }
            else
            {
                _mapper.Map(dto, entity);

                foreach (IFormFile file in dto.Files)
                {
                    entity.Images.Add(new TrainingJournalistImage
                    {
                        Image = await file.FileUpload(_env.WebRootPath, "training-journalists")
                    });
                }

                foreach (var v in dto.Descriptions)
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    dto.Titles.TryGetValue(v.Key, out var title);

                    var existingTranslation = entity.Translations?.FirstOrDefault(t => t.LanguageId == lang.Id);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Description = v.Value;
                    }
                    else
                    {
                        entity.Translations?.Add(new TrainingJournalistTranslation
                        {
                            LanguageId = lang.Id,
                            Description = v.Value,
                            Title = title,
                        });
                    }
                }
                result = await _repository.UpdateAsync(entity);
            }


            return result;
        }

        public async Task<bool> DeleteImageAsync(int entityId, int imageId)
        {
            var entity = await _repository.GetQuery()
                            .Include(b => b.Images)
                            .FirstOrDefaultAsync(b => b.Id == entityId);

            if (entity is null) return false;


            var image = entity.Images.FirstOrDefault(i => i.Id == imageId);
            if (image is null) return false;
            FileManager.FileDelete(_env.WebRootPath, image.Image);
            entity.Images.Remove(image);

            return await _repository.UpdateAsync(entity);
        }

        public async Task<TrainingJournalistGetDTO?> GetFirst()
        {
            TrainingJournalist? entity = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .Include(x => x.Images)
                .FirstOrDefaultAsync();

            if (entity is null) return null;

            TrainingJournalistGetDTO data = _mapper.Map<TrainingJournalistGetDTO>(entity);
            data.Images = _mapper.Map<List<TrainingJournalistImageDTO>>(entity.Images);

            return data;
        }
    }
}
