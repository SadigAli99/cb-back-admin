using AutoMapper;
using CB.Application.DTOs.CBAR105Event;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class CBAR105EventService : ICBAR105EventService
    {
        private readonly IGenericRepository<CBAR105Event> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public CBAR105EventService(
            IMapper mapper,
            IGenericRepository<CBAR105Event> repository,
            IGenericRepository<Language> languageRepository,
            IWebHostEnvironment env
        )
        {
            _env = env;
            _mapper = mapper;
            _repository = repository;
            _languageRepository = languageRepository;
        }

        public async Task<bool> CreateOrUpdate(CBAR105EventPostDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            CBAR105Event? entity = await _repository.GetQuery()
                                        .Include(h => h.Translations)
                                        .ThenInclude(ht => ht.Language)
                                        .FirstOrDefaultAsync();

            bool result;

            if (entity is null)
            {
                entity = _mapper.Map<CBAR105Event>(dto);

                entity.Translations = dto.Descriptions.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    return new CBAR105EventTranslation
                    {
                        LanguageId = lang.Id,
                        Description = v.Value,
                    };
                }).ToList();

                result = await _repository.AddAsync(entity);
            }
            else
            {
                _mapper.Map(dto, entity);

                foreach (var v in dto.Descriptions)
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    var existingTranslation = entity.Translations?.FirstOrDefault(t => t.LanguageId == lang.Id);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Description = v.Value;
                    }
                    else
                    {
                        entity.Translations?.Add(new CBAR105EventTranslation
                        {
                            LanguageId = lang.Id,
                            Description = v.Value
                        });
                    }
                }

                foreach (IFormFile file in dto.Files)
                {
                    entity.Images.Add(new CBAR105EventImage
                    {
                        Image = await file.FileUpload(_env.WebRootPath, "cbar105-events")
                    });
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

        public async Task<CBAR105EventGetDTO?> GetFirst()
        {
            CBAR105Event? entity = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .Include(x => x.Images)
                .FirstOrDefaultAsync();

            if (entity is null) return null;

            CBAR105EventGetDTO data = _mapper.Map<CBAR105EventGetDTO>(entity);
            data.Images = _mapper.Map<List<CBAR105EventImageDTO>>(entity.Images);

            return data;
        }
    }
}
