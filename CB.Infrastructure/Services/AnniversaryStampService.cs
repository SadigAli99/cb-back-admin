using AutoMapper;
using CB.Application.DTOs.AnniversaryStamp;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class AnniversaryStampService : IAnniversaryStampService
    {
        private readonly IGenericRepository<AnniversaryStamp> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public AnniversaryStampService(
            IGenericRepository<AnniversaryStamp> repository,
            IGenericRepository<Language> languageRepository,
            IFileService fileService,
            IMapper mapper
        )
        {
            _languageRepository = languageRepository;
            _repository = repository;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<bool> CreateOrUpdate(AnniversaryStampPostDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            AnniversaryStamp? entity = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync();

            bool result;

            if (entity is null)
            {
                entity = _mapper.Map<AnniversaryStamp>(dto);

                if (dto.File != null)
                {
                    entity.Image = await _fileService.UploadAsync(dto.File, "anniversary-stamps");
                }

                entity.Translations = dto.Titles.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    return new AnniversaryStampTranslation
                    {
                        LanguageId = lang.Id,
                        Title = v.Value
                    };
                }).ToList();

                result = await _repository.AddAsync(entity);
            }
            else
            {
                _mapper.Map(dto, entity);

                if (dto.File != null)
                {
                    entity.Image = await _fileService.UploadAsync(dto.File, "anniversary-stamps");
                    _fileService.Delete(entity.Image);
                }

                foreach (var v in dto.Titles)
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    var existingTranslation = entity.Translations.FirstOrDefault(t => t.LanguageId == lang.Id);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Title = v.Value;
                    }
                    else
                    {
                        entity.Translations.Add(new AnniversaryStampTranslation
                        {
                            LanguageId = lang.Id,
                            Title = v.Value
                        });
                    }
                }

                result = await _repository.UpdateAsync(entity);
            }

            return result;
        }

        public async Task<AnniversaryStampGetDTO?> GetFirst()
        {
            AnniversaryStamp? entity = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync();

            if (entity is null) return null;


            return _mapper.Map<AnniversaryStampGetDTO>(entity);
        }
    }
}
