
using AutoMapper;
using CB.Application.DTOs.CoinMoneySign;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class CoinMoneySignService : ICoinMoneySignService
    {
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IGenericRepository<MoneySign> _repository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public CoinMoneySignService(
            IGenericRepository<Language> languageRepository,
            IGenericRepository<MoneySign> repository,
            IFileService fileService,
            IMapper mapper
        )
        {
            _languageRepository = languageRepository;
            _fileService = fileService;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CoinMoneySignGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Where(x => x.Type == MoneySignType.COIN)
                        .ToListAsync();

            List<CoinMoneySignGetDTO> data = _mapper.Map<List<CoinMoneySignGetDTO>>(entities);
            return data;
        }
        public async Task<CoinMoneySignGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Where(x => x.Type == MoneySignType.COIN)
                        .FirstOrDefaultAsync(b => b.Id == id);

            CoinMoneySignGetDTO? data = entity is null ? null : _mapper.Map<CoinMoneySignGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(CoinMoneySignCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<MoneySign>(dto);
            entity.Type = MoneySignType.COIN;
            entity.Image = await _fileService.UploadAsync(dto.File, "money-signs");
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");



                return new MoneySignTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, CoinMoneySignEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            if (dto.File != null)
            {
                _fileService.Delete(entity.Image);
                entity.Image = await _fileService.UploadAsync(dto.File, "money-signs");
            }

            entity.Type = MoneySignType.COIN;

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                return new MoneySignTranslation
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
            _fileService.Delete(entity.Image);
            return await _repository.DeleteAsync(entity);
        }

    }
}
