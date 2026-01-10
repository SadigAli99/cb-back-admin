using AutoMapper;
using CB.Application.DTOs.StructureCaption;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class StructureCaptionService : IStructureCaptionService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IGenericRepository<StructureCaption> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public StructureCaptionService(
            IMapper mapper,
            IGenericRepository<StructureCaption> repository,
            IGenericRepository<Language> languageRepository,
            IWebHostEnvironment env
        )
        {
            _env = env;
            _mapper = mapper;
            _repository = repository;
            _languageRepository = languageRepository;
        }

        public async Task<bool> CreateOrUpdate(StructureCaptionPostDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            StructureCaption? entity = await _repository.GetQuery()
                                        .Include(h => h.Translations)
                                        .ThenInclude(ht => ht.Language)
                                        .FirstOrDefaultAsync();

            bool result;

            if (entity is null)
            {
                entity = _mapper.Map<StructureCaption>(dto);

                if (dto.File != null)
                {
                    FileManager.FileDelete(_env.WebRootPath, entity.Image ?? "");
                    entity.Image = await dto.File.FileUpload(_env.WebRootPath, "structures");
                }

                entity.Translations = dto.Descriptions.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    return new StructureCaptionTranslation
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

                if (dto.File != null)
                {
                    FileManager.FileDelete(_env.WebRootPath, entity.Image ?? "");
                    entity.Image = await dto.File.FileUpload(_env.WebRootPath, "structures");
                }

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
                        entity.Translations.Add(new StructureCaptionTranslation
                        {
                            LanguageId = lang.Id,
                            Description = v.Value
                        });
                    }
                }

                result = await _repository.UpdateAsync(entity);
            }


            return result;
        }

        public async Task<StructureCaptionGetDTO?> GetFirst()
        {
            StructureCaption entity = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync(h => h.Id == 1);

            return entity == null ? null : _mapper.Map<StructureCaptionGetDTO>(entity);
        }
    }
}
