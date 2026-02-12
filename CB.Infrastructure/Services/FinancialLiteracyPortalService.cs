
using AutoMapper;
using CB.Application.DTOs.FinancialLiteracyPortal;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class FinancialLiteracyPortalService : IFinancialLiteracyPortalService
    {
        private readonly IGenericRepository<FinancialLiteracyPortal> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public FinancialLiteracyPortalService(
            IGenericRepository<FinancialLiteracyPortal> repository,
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

        public async Task<List<FinancialLiteracyPortalGetDTO>> GetAllAsync()
        {
            var query = _repository.GetQuery();

            var entities = await query
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(b => b.Images)
                        .ToListAsync();


            List<FinancialLiteracyPortalGetDTO> data = _mapper.Map<List<FinancialLiteracyPortalGetDTO>>(entities);

            foreach (var entity in entities)
            {
                FinancialLiteracyPortalGetDTO? dTO = data.FirstOrDefault(x => x.Id == entity.Id);
                if (dTO is null) continue;
                dTO.Images = _mapper.Map<List<FinancialLiteracyPortalImageDTO>>(entity.Images);
            }
            return data;
        }
        public async Task<FinancialLiteracyPortalGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(b => b.Images)
                        .FirstOrDefaultAsync(b => b.Id == id);

            FinancialLiteracyPortalGetDTO? data = entity is null ? null : _mapper.Map<FinancialLiteracyPortalGetDTO>(entity);

            if (data != null)
            {
                data.Images = _mapper.Map<List<FinancialLiteracyPortalImageDTO>>(entity?.Images);
            }

            return data;
        }
        public async Task<bool> CreateAsync(FinancialLiteracyPortalCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<FinancialLiteracyPortal>(dto);
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");
                dto.Descriptions.TryGetValue(t.Key, out var description);

                return new FinancialLiteracyPortalTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Description = description,
                };
            }).ToList();

            foreach (IFormFile file in dto.ImageFiles)
            {
                entity.Images?.Add(new FinancialLiteracyPortalImage
                {
                    Image = await _fileService.UploadAsync(file, "financial-literacy-portals"),
                });
            }

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, FinancialLiteracyPortalEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(b => b.Images)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");
                dto.Descriptions.TryGetValue(t.Key, out var description);

                return new FinancialLiteracyPortalTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Description = description,
                };
            }).ToList();

            foreach (IFormFile file in dto.ImageFiles)
            {
                entity.Images?.Add(new FinancialLiteracyPortalImage
                {
                    Image = await _fileService.UploadAsync(file, "financial-literacy-portals"),
                });
            }



            return await _repository.UpdateAsync(entity);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null) return false;
            return await _repository.DeleteAsync(entity);
        }

        public async Task<bool> DeleteImageAsync(int financialLiteracyPortalId, int imageId)
        {
            var entity = await _repository.GetQuery()
                            .Include(b => b.Images)
                            .FirstOrDefaultAsync(b => b.Id == financialLiteracyPortalId);

            if (entity is null) return false;


            var image = entity.Images.FirstOrDefault(i => i.Id == imageId);
            if (image is null) return false;
            _fileService.Delete(image.Image);
            entity.Images.Remove(image);

            return await _repository.UpdateAsync(entity);
        }


    }
}
