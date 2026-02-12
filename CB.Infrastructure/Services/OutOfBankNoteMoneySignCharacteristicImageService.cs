
using AutoMapper;
using CB.Application.DTOs.OutOfBankNoteMoneySignCharacteristicImage;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CB.Infrastructure.Services
{
    public class OutOfBankNoteMoneySignCharacteristicImageService : IOutOfBankNoteMoneySignCharacteristicImageService
    {
        private readonly IGenericRepository<MoneySignCharacteristicImage> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IConfiguration _config;

        public OutOfBankNoteMoneySignCharacteristicImageService(
            IGenericRepository<MoneySignCharacteristicImage> repository,
            IGenericRepository<Language> languageRepository,
            IFileService fileService,
            IConfiguration config,
            IMapper mapper
        )
        {
            _languageRepository = languageRepository;
            _repository = repository;
            _mapper = mapper;
            _config = config;
            _fileService = fileService;
        }

        public async Task<List<OutOfBankNoteMoneySignCharacteristicImageGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(x => x.MoneySignHistory)
                        .ThenInclude(x => x.Translations)
                        .Include(x => x.MoneySignHistory)
                        .ThenInclude(x => x.MoneySign)
                        .Where(x => x.MoneySignHistory.MoneySign.Type == MoneySignType.BANKNOTEOUT)
                        .ToListAsync();

            List<OutOfBankNoteMoneySignCharacteristicImageGetDTO> data = _mapper.Map<List<OutOfBankNoteMoneySignCharacteristicImageGetDTO>>(entities);
            foreach (OutOfBankNoteMoneySignCharacteristicImageGetDTO item in data)
            {
                var entity = entities.FirstOrDefault(x => x.Id == item.Id);
                if (entity is null) continue;
                item.FrontImage = $"{_config["BaseUrl"]}{entity.FrontImage}";
                item.BackImage = $"{_config["BaseUrl"]}{entity.BackImage}";
                item.MoneySignHistoryTitle = entity.MoneySignHistory.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            }
            return data;
        }
        public async Task<OutOfBankNoteMoneySignCharacteristicImageGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(x => x.MoneySignHistory)
                        .ThenInclude(x => x.Translations)
                        .Include(x => x.MoneySignHistory)
                        .ThenInclude(x => x.MoneySign)
                        .Where(x => x.MoneySignHistory.MoneySign.Type == MoneySignType.BANKNOTEOUT)
                        .FirstOrDefaultAsync(b => b.Id == id);

            if (entity is null) return null;

            OutOfBankNoteMoneySignCharacteristicImageGetDTO? data = _mapper.Map<OutOfBankNoteMoneySignCharacteristicImageGetDTO>(entity);
            data.FrontImage = $"{_config["BaseUrl"]}{entity.FrontImage}";
            data.BackImage = $"{_config["BaseUrl"]}{entity.BackImage}";
            data.MoneySignHistoryTitle = entity.MoneySignHistory.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            return data;
        }
        public async Task<bool> CreateAsync(OutOfBankNoteMoneySignCharacteristicImageCreateDTO dto)
        {
            var entity = _mapper.Map<MoneySignCharacteristicImage>(dto);
            entity.FrontImage = await _fileService.UploadAsync(dto.FrontFile, "money-sign-characteristics");
            entity.BackImage = await _fileService.UploadAsync(dto.BackFile, "money-sign-characteristics");


            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Colors.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Sizes.TryGetValue(t.Key, out var size);

                return new MoneySignCharacteristicImageTranslation
                {
                    LanguageId = lang.Id,
                    Color = t.Value,
                    Size = size
                };
            }).ToList();
            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, OutOfBankNoteMoneySignCharacteristicImageEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            if (dto.FrontFile != null)
            {
                _fileService.Delete(entity.FrontImage ?? "");
                entity.FrontImage = await _fileService.UploadAsync(dto.FrontFile, "money-sign-characteristics");
            }

            if (dto.BackFile != null)
            {
                _fileService.Delete(entity.BackImage ?? "");
                entity.BackImage = await _fileService.UploadAsync(dto.BackFile, "money-sign-characteristics");
            }


            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Colors.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Sizes.TryGetValue(t.Key, out var size);

                return new MoneySignCharacteristicImageTranslation
                {
                    LanguageId = lang.Id,
                    Color = t.Value,
                    Size = size
                };
            }).ToList();


            return await _repository.UpdateAsync(entity);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null) return false;
            _fileService.Delete(entity.FrontImage ?? "");
            _fileService.Delete(entity.BackImage ?? "");
            return await _repository.DeleteAsync(entity);
        }

    }
}
