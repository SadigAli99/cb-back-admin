
using AutoMapper;
using CB.Application.DTOs.NationalBankNoteMoneySign;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class NationalBankNoteMoneySignService : INationalBankNoteMoneySignService
    {
        private readonly IGenericRepository<MoneySign> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public NationalBankNoteMoneySignService(
            IGenericRepository<MoneySign> repository,
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

        public async Task<List<NationalBankNoteMoneySignGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Where(x => x.Type == MoneySignType.BANKNOTE)
                        .ToListAsync();

            List<NationalBankNoteMoneySignGetDTO> data = _mapper.Map<List<NationalBankNoteMoneySignGetDTO>>(entities);
            return data;
        }
        public async Task<NationalBankNoteMoneySignGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Where(x => x.Type == MoneySignType.BANKNOTE)
                        .FirstOrDefaultAsync(b => b.Id == id);

            NationalBankNoteMoneySignGetDTO? data = entity is null ? null : _mapper.Map<NationalBankNoteMoneySignGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(NationalBankNoteMoneySignCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<MoneySign>(dto);
            entity.Type = MoneySignType.BANKNOTE;
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

        public async Task<bool> UpdateAsync(int id, NationalBankNoteMoneySignEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            if (dto.File != null)
            {
                _fileService.Delete( entity.Image ?? "");
                entity.Image = await _fileService.UploadAsync(dto.File, "money-signs");
            }

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
            _fileService.Delete( entity.Image ?? "");
            return await _repository.DeleteAsync(entity);
        }

    }
}
