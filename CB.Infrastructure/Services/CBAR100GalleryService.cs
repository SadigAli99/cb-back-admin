
using AutoMapper;
using CB.Application.DTOs.CBAR100Gallery;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Extensions;
using CB.Shared.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class CBAR100GalleryService : ICBAR100GalleryService
    {
        private readonly IGenericRepository<CBAR100Gallery> _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public CBAR100GalleryService(
            IGenericRepository<CBAR100Gallery> repository,
            IWebHostEnvironment env,
            IMapper mapper
        )
        {
            _repository = repository;
            _mapper = mapper;
            _env = env;
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
            entity.Image = await dto.File.FileUpload(_env.WebRootPath, "cbar100-galleries");

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, CBAR100GalleryEditDTO dto)
        {
            var entity = await _repository.GetQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) return false;

            _mapper.Map(dto, entity);

            if (dto.File != null)
            {
                FileManager.FileDelete(_env.WebRootPath, entity.Image ?? "");
                entity.Image = await dto.File.FileUpload(_env.WebRootPath, "cbar100-galleries");
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
