
using AutoMapper;
using CB.Application.DTOs.OutOfBankNoteMoneySign;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class OutOfBankNoteMoneySignService : IOutOfBankNoteMoneySignService
    {
        private readonly IGenericRepository<MoneySign> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public OutOfBankNoteMoneySignService(
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

        public async Task<List<OutOfBankNoteMoneySignGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Where(x => x.Type == MoneySignType.BANKNOTEOUT)
                        .ToListAsync();

            List<OutOfBankNoteMoneySignGetDTO> data = _mapper.Map<List<OutOfBankNoteMoneySignGetDTO>>(entities);
            return data;
        }
        public async Task<OutOfBankNoteMoneySignGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Where(x => x.Type == MoneySignType.BANKNOTEOUT)
                        .FirstOrDefaultAsync(b => b.Id == id);

            OutOfBankNoteMoneySignGetDTO? data = entity is null ? null : _mapper.Map<OutOfBankNoteMoneySignGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(OutOfBankNoteMoneySignCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<MoneySign>(dto);
            entity.Type = MoneySignType.BANKNOTEOUT;
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

        public async Task<bool> UpdateAsync(int id, OutOfBankNoteMoneySignEditDTO dto)
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

            entity.Type = MoneySignType.BANKNOTEOUT;

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
