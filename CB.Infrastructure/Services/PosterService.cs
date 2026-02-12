
using AutoMapper;
using CB.Application.DTOs.Poster;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class PosterService : IPosterService
    {
        private readonly IGenericRepository<Poster> _repository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public PosterService(
            IGenericRepository<Poster> repository,
            IFileService fileService,
            IMapper mapper
        )
        {
            _repository = repository;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<List<PosterGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery().ToListAsync();

            List<PosterGetDTO> data = _mapper.Map<List<PosterGetDTO>>(entities);
            return data;
        }
        public async Task<PosterGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            PosterGetDTO? data = entity is null ? null : _mapper.Map<PosterGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(PosterCreateDTO dto)
        {
            var entity = _mapper.Map<Poster>(dto);
            entity.Image = await _fileService.UploadAsync(dto.File, "posters");

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, PosterEditDTO dto)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);
            if(dto.File != null)
            {
                _fileService.Delete( entity.Image ?? "");
                entity.Image = await _fileService.UploadAsync(dto.File, "posters");
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
