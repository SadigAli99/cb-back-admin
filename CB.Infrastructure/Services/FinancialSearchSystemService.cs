
using AutoMapper;
using CB.Application.DTOs.FinancialSearchSystem;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Extensions;
using CB.Shared.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class FinancialSearchSystemService : IFinancialSearchSystemService
    {
        private readonly IGenericRepository<FinancialSearchSystem> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public FinancialSearchSystemService(
            IGenericRepository<FinancialSearchSystem> repository,
            IGenericRepository<Language> languageRepository,
            IWebHostEnvironment env,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
            _env = env;
        }

        public async Task<List<FinancialSearchSystemGetDTO>> GetAllAsync()
        {
            var query = _repository.GetQuery();

            var entities = await query
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(b => b.Images)
                        .ToListAsync();


            List<FinancialSearchSystemGetDTO> data = _mapper.Map<List<FinancialSearchSystemGetDTO>>(entities);

            foreach (var entity in entities)
            {
                FinancialSearchSystemGetDTO? dTO = data.FirstOrDefault(x => x.Id == entity.Id);
                if (dTO is null) continue;
                dTO.Images = _mapper.Map<List<FinancialSearchSystemImageDTO>>(entity.Images);
            }
            return data;
        }
        public async Task<FinancialSearchSystemGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(b => b.Images)
                        .FirstOrDefaultAsync(b => b.Id == id);

            FinancialSearchSystemGetDTO? data = entity is null ? null : _mapper.Map<FinancialSearchSystemGetDTO>(entity);

            if (data != null)
            {
                data.Images = _mapper.Map<List<FinancialSearchSystemImageDTO>>(entity?.Images);
            }

            return data;
        }
        public async Task<bool> CreateAsync(FinancialSearchSystemCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<FinancialSearchSystem>(dto);
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");
                dto.Descriptions.TryGetValue(t.Key, out var description);

                return new FinancialSearchSystemTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Description = description,
                };
            }).ToList();

            foreach (IFormFile file in dto.ImageFiles)
            {
                entity.Images?.Add(new FinancialSearchSystemImage
                {
                    Image = await file.FileUpload(_env.WebRootPath, "financial-search-systems"),
                });
            }

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, FinancialSearchSystemEditDTO dto)
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

                return new FinancialSearchSystemTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Description = description,
                };
            }).ToList();

            foreach (IFormFile file in dto.ImageFiles)
            {
                entity.Images?.Add(new FinancialSearchSystemImage
                {
                    Image = await file.FileUpload(_env.WebRootPath, "financial-search-systems"),
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
            FileManager.FileDelete(_env.WebRootPath, image.Image);
            entity.Images.Remove(image);

            return await _repository.UpdateAsync(entity);
        }


    }
}
