
using AutoMapper;
using CB.Application.DTOs.MoneySignProtectionElement;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class MoneySignProtectionElementService : IMoneySignProtectionElementService
    {
        private readonly IGenericRepository<MoneySignProtectionElement> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public MoneySignProtectionElementService(
            IGenericRepository<MoneySignProtectionElement> repository,
            IGenericRepository<Language> languageRepository,
            IFileService fileService,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<List<MoneySignProtectionElementGetDTO>> GetAllAsync(int? id)
        {
            var query = _repository.GetQuery();

            if (id != default(int)) query = query.Where(x => x.MoneySignHistoryId == id);

            var entities = await query
                        .Include(x => x.MoneySignHistory)
                        .ThenInclude(x => x.Translations)
                        .ThenInclude(x => x.Language)
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(b => b.Images)
                        .ToListAsync();


            List<MoneySignProtectionElementGetDTO> data = _mapper.Map<List<MoneySignProtectionElementGetDTO>>(entities);

            foreach (var entity in entities)
            {
                MoneySignProtectionElementGetDTO? dTO = data.FirstOrDefault(x => x.Id == entity.Id);
                if (dTO is null) continue;
                dTO.Images = _mapper.Map<List<MoneySignProtectionElementImageDTO>>(entity.Images);
                dTO.MoneySignHistoryTitle = entity.MoneySignHistory.Translations.FirstOrDefault()?.Title;
            }
            return data;
        }
        public async Task<MoneySignProtectionElementGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(b => b.Images)
                        .FirstOrDefaultAsync(b => b.Id == id);

            MoneySignProtectionElementGetDTO? data = entity is null ? null : _mapper.Map<MoneySignProtectionElementGetDTO>(entity);

            if (data != null)
            {
                data.Images = _mapper.Map<List<MoneySignProtectionElementImageDTO>>(entity?.Images);
                data.MoneySignHistoryTitle = entity?.MoneySignHistory.Translations.FirstOrDefault()?.Title;
            }

            return data;
        }
        public async Task<bool> CreateAsync(MoneySignProtectionElementCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<MoneySignProtectionElement>(dto);
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");
                dto.Values.TryGetValue(t.Key, out var value);

                return new MoneySignProtectionElementTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Value = value,
                };
            }).ToList();

            foreach (IFormFile file in dto.ImageFiles)
            {
                entity.Images?.Add(new MoneySignProtectionElementImage
                {
                    Image = await _fileService.UploadAsync(file, "money-sign-protection-elements"),
                });
            }

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, MoneySignProtectionElementEditDTO dto)
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

                dto.Values.TryGetValue(t.Key, out var value);

                return new MoneySignProtectionElementTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Value = value,
                };
            }).ToList();

            foreach (IFormFile file in dto.ImageFiles)
            {
                entity.Images?.Add(new MoneySignProtectionElementImage
                {
                    Image = await _fileService.UploadAsync(file, "money-sign-protection-elements"),
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

        public async Task<bool> DeleteImageAsync(int moneySignProtectionElementId, int imageId)
        {
            var entity = await _repository.GetQuery()
                            .Include(b => b.Images)
                            .FirstOrDefaultAsync(b => b.Id == moneySignProtectionElementId);

            if (entity is null) return false;


            var image = entity.Images.FirstOrDefault(i => i.Id == imageId);
            if (image is null) return false;
            _fileService.Delete(image.Image ?? "");
            entity.Images.Remove(image);

            return await _repository.UpdateAsync(entity);
        }


    }
}
