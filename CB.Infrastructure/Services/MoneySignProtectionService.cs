
using AutoMapper;
using CB.Application.DTOs.MoneySignProtection;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CB.Infrastructure.Services
{
    public class MoneySignProtectionService : IMoneySignProtectionService
    {
        private readonly IGenericRepository<MoneySignProtection> _repository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IConfiguration _config;

        public MoneySignProtectionService(
            IGenericRepository<MoneySignProtection> repository,
            IFileService fileService,
            IConfiguration config,
            IMapper mapper
        )
        {
            _repository = repository;
            _mapper = mapper;
            _config = config;
            _fileService = fileService;
        }

        public async Task<List<MoneySignProtectionGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(x => x.MoneySignHistory)
                        .ThenInclude(x => x.Translations)
                        .ToListAsync();

            List<MoneySignProtectionGetDTO> data = _mapper.Map<List<MoneySignProtectionGetDTO>>(entities);
            foreach (MoneySignProtectionGetDTO item in data)
            {
                var entity = entities.FirstOrDefault(x => x.Id == item.Id);
                if (entity is null) continue;
                item.Image = $"{_config["BaseUrl"]}{entity.Image}";
                item.MoneySignHistoryTitle = entity.MoneySignHistory.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            }
            return data;
        }
        public async Task<MoneySignProtectionGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(x => x.MoneySignHistory)
                        .ThenInclude(x => x.Translations)
                        .FirstOrDefaultAsync(b => b.Id == id);

            if (entity is null) return null;

            MoneySignProtectionGetDTO? data = _mapper.Map<MoneySignProtectionGetDTO>(entity);
            data.Image = $"{_config["BaseUrl"]}{entity.Image}";
            data.MoneySignHistoryTitle = entity.MoneySignHistory.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            return data;
        }
        public async Task<bool> CreateAsync(MoneySignProtectionCreateDTO dto)
        {
            var entity = _mapper.Map<MoneySignProtection>(dto);
            entity.Image = await _fileService.UploadAsync(dto.File, "money-sign-protections");

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, MoneySignProtectionEditDTO dto)
        {
            var entity = await _repository.GetQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            if (dto.File != null)
            {
                _fileService.Delete( entity.Image ?? "");
                entity.Image = await _fileService.UploadAsync(dto.File, "money-sign-characteristics");
            }



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
