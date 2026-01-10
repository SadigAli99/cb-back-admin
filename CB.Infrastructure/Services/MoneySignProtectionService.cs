
using AutoMapper;
using CB.Application.DTOs.MoneySignProtection;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CB.Infrastructure.Services
{
    public class MoneySignProtectionService : IMoneySignProtectionService
    {
        private readonly IGenericRepository<MoneySignProtection> _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

        public MoneySignProtectionService(
            IGenericRepository<MoneySignProtection> repository,
            IWebHostEnvironment env,
            IConfiguration config,
            IMapper mapper
        )
        {
            _repository = repository;
            _mapper = mapper;
            _config = config;
            _env = env;
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
            entity.Image = await dto.File.FileUpload(_env.WebRootPath, "money-sign-protections");

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, MoneySignProtectionEditDTO dto)
        {
            var entity = await _repository.GetQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            if (dto.File != null)
            {
                FileManager.FileDelete(_env.WebRootPath, entity.Image ?? "");
                entity.Image = await dto.File.FileUpload(_env.WebRootPath, "money-sign-characteristics");
            }



            return await _repository.UpdateAsync(entity);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null) return false;
            FileManager.FileDelete(_env.WebRootPath, entity.Image ?? "");
            return await _repository.DeleteAsync(entity);
        }

    }
}
