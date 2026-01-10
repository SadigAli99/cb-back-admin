using AutoMapper;
using CB.Application.DTOs.AnniversaryCoin;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class AnniversaryCoinService : IAnniversaryCoinService
    {
        private readonly IGenericRepository<AnniversaryCoin> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public AnniversaryCoinService(
            IMapper mapper,
            IGenericRepository<AnniversaryCoin> repository,
            IGenericRepository<Language> languageRepository,
            IWebHostEnvironment env
        )
        {
            _env = env;
            _mapper = mapper;
            _repository = repository;
            _languageRepository = languageRepository;
        }

        public async Task<bool> CreateOrUpdate(AnniversaryCoinPostDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            AnniversaryCoin? entity = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync();

            bool result;

            if (entity is null)
            {
                entity = _mapper.Map<AnniversaryCoin>(dto);

                if (dto.File != null)
                {
                    entity.Image = await dto.File.FileUpload(_env.WebRootPath, "anniversary-coins");
                }

                entity.Translations = dto.Titles.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    dto.Descriptions.TryGetValue(v.Key, out var description);

                    return new AnniversaryCoinTranslation
                    {
                        LanguageId = lang.Id,
                        Title = v.Value,
                        Description = description,
                    };
                }).ToList();

                result = await _repository.AddAsync(entity);
            }
            else
            {
                _mapper.Map(dto, entity);

                if (dto.File != null)
                {
                    entity.Image = await dto.File.FileUpload(_env.WebRootPath, "anniversary-coins");
                    FileManager.FileDelete(_env.WebRootPath, entity.Image ?? "");
                }

                foreach (var v in dto.Titles)
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    dto.Descriptions.TryGetValue(v.Key, out var description);

                    var existingTranslation = entity.Translations?.FirstOrDefault(t => t.LanguageId == lang.Id);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Title = v.Value;
                    }
                    else
                    {
                        entity.Translations?.Add(new AnniversaryCoinTranslation
                        {
                            LanguageId = lang.Id,
                            Title = v.Value,
                            Description = description,
                        });
                    }
                }

                result = await _repository.UpdateAsync(entity);
            }

            return result;
        }

        public async Task<AnniversaryCoinGetDTO?> GetFirst()
        {
            AnniversaryCoin? entity = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync();

            if (entity is null) return null;


            return _mapper.Map<AnniversaryCoinGetDTO>(entity);
        }
    }
}
