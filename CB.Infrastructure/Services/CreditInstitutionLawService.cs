
using AutoMapper;
using CB.Application.DTOs.CreditInstitutionLaw;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class CreditInstitutionLawService : ICreditInstitutionLawService
    {
        private readonly IGenericRepository<CreditInstitutionLaw> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public CreditInstitutionLawService(
            IGenericRepository<CreditInstitutionLaw> repository,
            IGenericRepository<Language> languageRepository,
            IFileService fileService,
            IMapper mapper
        )
        {
            _languageRepository = languageRepository;
            _fileService = fileService;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CreditInstitutionLawGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<CreditInstitutionLawGetDTO> data = _mapper.Map<List<CreditInstitutionLawGetDTO>>(entities);
            return data;
        }
        public async Task<CreditInstitutionLawGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            CreditInstitutionLawGetDTO? data = entity is null ? null : _mapper.Map<CreditInstitutionLawGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(CreditInstitutionLawCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<CreditInstitutionLaw>(dto);
            entity.File = await _fileService.UploadAsync(dto.File, "credit-institution-laws");
            entity.FileType = Path.GetExtension(dto.File.FileName)?.TrimStart('.');
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");



                return new CreditInstitutionLawTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, CreditInstitutionLawEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            if (dto.File != null)
            {
                _fileService.Delete(entity.File);
                entity.File = await _fileService.UploadAsync(dto.File, "credit-institution-laws");
                entity.FileType = Path.GetExtension(dto.File.FileName)?.TrimStart('.');
            }

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                return new CreditInstitutionLawTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                };
            }).ToList();



            return await _repository.UpdateAsync(entity);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null) return false;
            _fileService.Delete(entity.File);
            return await _repository.DeleteAsync(entity);
        }

    }
}
