
using AutoMapper;
using CB.Application.DTOs.CBAR100Gallery;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class CBAR100GalleryService : ICBAR100GalleryService
    {
        private readonly IGenericRepository<CBAR100Gallery> _repository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public CBAR100GalleryService(
            IGenericRepository<CBAR100Gallery> repository,
            IFileService fileService,
            IMapper mapper
        )
        {
            _fileService = fileService;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CBAR100GalleryGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery().ToListAsync();

            List<CBAR100GalleryGetDTO> data = _mapper.Map<List<CBAR100GalleryGetDTO>>(entities);
            return data;
        }
        public async Task<CBAR100GalleryGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery().FirstOrDefaultAsync(b => b.Id == id);

            if (entity is null) return null;

            CBAR100GalleryGetDTO? data = _mapper.Map<CBAR100GalleryGetDTO>(entity);

            return data;
        }
        public async Task<bool> CreateAsync(CBAR100GalleryCreateDTO dto)
        {
            var entity = _mapper.Map<CBAR100Gallery>(dto);
            entity.Image = await _fileService.UploadAsync(dto.File, "cbar100-galleries");

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, CBAR100GalleryEditDTO dto)
        {
            var entity = await _repository.GetQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            if (dto.File != null)
            {
                _fileService.Delete(entity.Image);
                entity.Image = await _fileService.UploadAsync(dto.File, "cbar100-galleries");
            }

            return await _repository.UpdateAsync(entity);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null) return false;
            _fileService.Delete(entity.Image);
            return await _repository.DeleteAsync(entity);
        }

    }
}
