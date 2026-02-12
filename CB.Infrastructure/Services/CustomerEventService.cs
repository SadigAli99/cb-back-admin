
using AutoMapper;
using CB.Application.DTOs.CustomerEvent;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class CustomerEventService : ICustomerEventService
    {
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IGenericRepository<CustomerEvent> _repository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public CustomerEventService(
            IGenericRepository<Language> languageRepository,
            IGenericRepository<CustomerEvent> repository,
            IFileService fileService,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<List<CustomerEventGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(b => b.Images)
                        .ToListAsync();

            List<CustomerEventGetDTO> data = _mapper.Map<List<CustomerEventGetDTO>>(entities);

            foreach (var entity in entities)
            {
                CustomerEventGetDTO? dTO = data.FirstOrDefault(x => x.Id == entity.Id);
                if (dTO is null) continue;
                dTO.Images = _mapper.Map<List<CustomerEventImageDTO>>(entity.Images);
            }
            return data;
        }
        public async Task<CustomerEventGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .Include(b => b.Images)
                        .FirstOrDefaultAsync(b => b.Id == id);

            CustomerEventGetDTO? data = entity is null ? null : _mapper.Map<CustomerEventGetDTO>(entity);

            if (data != null) data.Images = _mapper.Map<List<CustomerEventImageDTO>>(entity?.Images);

            return data;
        }
        public async Task<bool> CreateAsync(CustomerEventCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            var entity = _mapper.Map<CustomerEvent>(dto);
            entity.Translations = dto.Titles.Select(t =>
            {
                var lang = languages.FirstOrDefault(x => x.Code == t.Key);
                if (lang is null)
                    throw new Exception($"{t.Key} kodu ilə dil tapılmadı");

                dto.Descriptions.TryGetValue(t.Key, out var description);

                return new CustomerEventTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Description = description
                };
            }).ToList();

            foreach (IFormFile file in dto.Files)
            {
                entity.Images?.Add(new CustomerEventImage
                {
                    Image = await _fileService.UploadAsync(file, "customer-events"),
                });
            }

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, CustomerEventEditDTO dto)
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

                return new CustomerEventTranslation
                {
                    LanguageId = lang.Id,
                    Title = t.Value,
                    Description = description,
                };
            }).ToList();

            foreach (IFormFile file in dto.Files)
            {
                entity.Images?.Add(new CustomerEventImage
                {
                    Image = await _fileService.UploadAsync(file, "galleries"),
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

        public async Task<bool> DeleteImageAsync(int blogId, int imageId)
        {
            var entity = await _repository.GetQuery()
                            .Include(b => b.Images)
                            .FirstOrDefaultAsync(b => b.Id == blogId);

            if (entity is null) return false;


            var image = entity.Images.FirstOrDefault(i => i.Id == imageId);
            if (image is null) return false;
            _fileService.Delete(image.Image);
            entity.Images.Remove(image);

            return await _repository.UpdateAsync(entity);
        }


    }
}
