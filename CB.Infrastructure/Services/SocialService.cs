using AutoMapper;
using CB.Application.DTOs.Social;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;

namespace CB.Infrastructure.Services
{
    public class SocialService : ISocialService
    {
        private readonly IGenericRepository<Social> _repository;
        private readonly IMapper _mapper;

        public SocialService(IGenericRepository<Social> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<SocialGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<SocialGetDTO>>(entities);
        }

        public async Task<SocialGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null)
                return null;

            return _mapper.Map<SocialGetDTO>(entity);
        }

        public async Task<bool> CreateAsync(SocialCreateDTO dto)
        {
            var entity = _mapper.Map<Social>(dto);
            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, SocialEditDTO dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null)
                return false;

            _mapper.Map(dto, entity);
            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null)
                return false;


            return await _repository.DeleteAsync(entity); ;
        }

        public async Task<SocialEditDTO?> GetForEditAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity is null ? null : _mapper.Map<SocialEditDTO>(entity);
        }
    }
}
