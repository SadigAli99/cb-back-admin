
using AutoMapper;
using CB.Application.DTOs.CustomerDocumentFile;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class CustomerDocumentFileService : ICustomerDocumentFileService
    {
        private readonly IGenericRepository<CustomerDocumentFile> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public CustomerDocumentFileService(
            IGenericRepository<CustomerDocumentFile> repository,
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

        public async Task<List<CustomerDocumentFileGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(x => x.CustomerDocument)
                        .ThenInclude(x => x.Translations)
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<CustomerDocumentFileGetDTO> data = _mapper.Map<List<CustomerDocumentFileGetDTO>>(entities);

            foreach (var item in data)
            {
                var entity = entities.FirstOrDefault(x => x.Id == item.Id);
                item.CustomerDocumentTitle = entity?.CustomerDocument?.Translations.FirstOrDefault()?.Title;
            }
            return data;
        }
        public async Task<CustomerDocumentFileGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(x => x.CustomerDocument)
                        .ThenInclude(x => x.Translations)
                        .FirstOrDefaultAsync(b => b.Id == id);
            if (entity is null) return null;

            CustomerDocumentFileGetDTO? data = _mapper.Map<CustomerDocumentFileGetDTO>(entity);
            data.CustomerDocumentTitle = entity?.CustomerDocument.Translations.FirstOrDefault()?.Title;
            return data;
        }
        public async Task<bool> CreateAsync(CustomerDocumentFileCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<CustomerDocumentFile>(dto);
            entity.File = await _fileService.UploadAsync(dto.File, "customer-documents");
            entity.FileType = Path.GetExtension(dto.File.FileName)?.TrimStart('.');
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");



                return new CustomerDocumentFileTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                };
            }).ToList();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, CustomerDocumentFileEditDTO dto)
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
                entity.File = await _fileService.UploadAsync(dto.File, "customer-documents");
                entity.FileType = Path.GetExtension(dto.File.FileName)?.TrimStart('.');
            }

            var languages = await _languageRepository.GetAllAsync();

            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);

                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                return new CustomerDocumentFileTranslation
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
