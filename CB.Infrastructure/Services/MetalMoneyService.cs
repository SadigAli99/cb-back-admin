
using AutoMapper;
using CB.Application.DTOs.MetalMoney;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class MetalMoneyService : IMetalMoneyService
    {
        private readonly IGenericRepository<Money> _repository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public MetalMoneyService(
            IGenericRepository<Money> repository,
            IFileService fileService,
            IMapper mapper
        )
        {
            _repository = repository;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<List<MetalMoneyGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .ToListAsync();

            List<MetalMoneyGetDTO> data = _mapper.Map<List<MetalMoneyGetDTO>>(entities);
            return data;
        }
        public async Task<MetalMoneyGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(b => b.Translations)
                        .ThenInclude(bt => bt.Language)
                        .FirstOrDefaultAsync(b => b.Id == id);

            MetalMoneyGetDTO? data = entity is null ? null : _mapper.Map<MetalMoneyGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(MetalMoneyCreateDTO dto)
        {
            var entity = _mapper.Map<Money>(dto);
            entity.Image = await _fileService.UploadAsync(dto.File, "moneys");
            entity.Type = MoneyType.METAL;

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, MetalMoneyEditDTO dto)
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
                entity.Image = await _fileService.UploadAsync(dto.File, "moneys");
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
