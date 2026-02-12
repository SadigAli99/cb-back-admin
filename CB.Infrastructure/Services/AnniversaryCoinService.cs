using AutoMapper;
using CB.Application.DTOs.AnniversaryCoin;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class AnniversaryCoinService : IAnniversaryCoinService
    {
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IGenericRepository<AnniversaryCoin> _repository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public AnniversaryCoinService(
            IGenericRepository<Language> languageRepository,
            IGenericRepository<AnniversaryCoin> repository,
            IFileService fileService,
            IMapper mapper
        )
        {
            _languageRepository = languageRepository;
            _fileService = fileService;
            _repository = repository;
            _mapper = mapper;
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
                    entity.Image = await _fileService.UploadAsync(dto.File, "anniversary-coins");
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
                    entity.Image = await _fileService.UploadAsync(dto.File, "anniversary-coins");
                    _fileService.Delete(entity.Image);
                }

                foreach (var v in dto.Titles)
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    dto.Descriptions.TryGetValue(v.Key, out var description);

                    var existingTranslation = entity.Translations.FirstOrDefault(t => t.LanguageId == lang.Id);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Title = v.Value;
                    }
                    else
                    {
                        entity.Translations.Add(new AnniversaryCoinTranslation
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
