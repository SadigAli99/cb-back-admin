
using AutoMapper;
using CB.Application.DTOs.ReceptionCitizenVideo;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class ReceptionCitizenVideoService : IReceptionCitizenVideoService
    {
        private readonly IGenericRepository<ReceptionCitizenVideo> _repository;
        private readonly IMapper _mapper;

        public ReceptionCitizenVideoService(
            IGenericRepository<ReceptionCitizenVideo> repository,
            IMapper mapper
        )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ReceptionCitizenVideoGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                        .Include(x => x.ReceptionCitizenCategory)
                        .ThenInclude(x => x.Translations)
                        .ToListAsync();

            List<ReceptionCitizenVideoGetDTO> data = _mapper.Map<List<ReceptionCitizenVideoGetDTO>>(entities);

            foreach (ReceptionCitizenVideoGetDTO item in data)
            {
                var entity = entities.FirstOrDefault(x => x.Id == item.Id);
                if (entity is null) continue;
                item.ReceptionCitizenCategoryTitle = entity.ReceptionCitizenCategory.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            }
            return data;
        }
        public async Task<ReceptionCitizenVideoGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                        .Include(x => x.ReceptionCitizenCategory)
                        .ThenInclude(x => x.Translations)
                        .FirstOrDefaultAsync(b => b.Id == id);

            if (entity is null) return null;

            ReceptionCitizenVideoGetDTO? data = _mapper.Map<ReceptionCitizenVideoGetDTO>(entity);
            data.ReceptionCitizenCategoryTitle = entity.ReceptionCitizenCategory.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;
            return data;
        }
        public async Task<bool> CreateAsync(ReceptionCitizenVideoCreateDTO dto)
        {
            var entity = _mapper.Map<ReceptionCitizenVideo>(dto);
            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, ReceptionCitizenVideoEditDTO dto)
        {
            var entity = await _repository.GetQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            return await _repository.UpdateAsync(entity);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null) return false;
            return await _repository.DeleteAsync(entity);
        }

    }
}
