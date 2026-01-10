
using AutoMapper;
using CB.Application.DTOs.PaymentSystemControlFile;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class PaymentSystemControlFileService : IPaymentSystemControlFileService
    {
        private readonly IGenericRepository<PaymentSystemControlFile> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public PaymentSystemControlFileService(
            IGenericRepository<PaymentSystemControlFile> repository,
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

        public async Task<List<PaymentSystemControlFileGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<PaymentSystemControlFileGetDTO> data = _mapper.Map<List<PaymentSystemControlFileGetDTO>>(entities);
            return data;
        }
        public async Task<PaymentSystemControlFileGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            PaymentSystemControlFileGetDTO? data = entity is null ? null : _mapper.Map<PaymentSystemControlFileGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(PaymentSystemControlFileCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<PaymentSystemControlFile>(dto);
            entity.File = await dto.File.FileUpload(_env.WebRootPath, "payment-system-controls");
            entity.FileType = Path.GetExtension(dto.File.FileName)?.TrimStart('.');
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.CoverTitles.TryGetValue(t.Key, out var coverTitle);


                return new PaymentSystemControlFileTranslation
                {
                    LanguageId = lang.Id,
                    CoverTitle = coverTitle,
                    Title = t.Value,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, PaymentSystemControlFileEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            if (dto.File != null)
            {
                FileManager.FileDelete(_env.WebRootPath, entity.File ?? "");
                entity.File = await dto.File.FileUpload(_env.WebRootPath, "payment-system-controls");
                entity.FileType = Path.GetExtension(dto.File.FileName)?.TrimStart('.');
            }

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);

                dto.CoverTitles.TryGetValue(t.Key, out var coverTitle);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                return new PaymentSystemControlFileTranslation
                {
                    LanguageId = lang.Id,
                    CoverTitle = coverTitle,
                    Title = t.Value,
                };
            }).ToList();



            return await _repository.UpdateAsync(entity);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null) return false;
            FileManager.FileDelete(_env.WebRootPath, entity.File ?? "");
            return await _repository.DeleteAsync(entity);
        }

    }
}
