using AutoMapper;
using CB.Application.DTOs.Logo;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class LogoService : ILogoService
    {
        private readonly IGenericRepository<Logo> _repository;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public LogoService(
            IMapper mapper,
            IGenericRepository<Logo> repository,
            IWebHostEnvironment env
        )
        {
            _mapper = mapper;
            _repository = repository;
            _env = env;
        }

        public async Task<bool> CreateOrUpdate(LogoPostDTO dto)
        {
            Logo? logo = await _repository.GetQuery().FirstOrDefaultAsync();

            bool result;

            if (logo is null)
            {
                logo = new Logo();
                logo.HeaderLogo = await dto.HeaderLogo?.FileUpload(_env.WebRootPath, "logos");
                logo.FooterLogo = await dto.FooterLogo?.FileUpload(_env.WebRootPath, "logos");
                logo.Favicon = await dto.Favicon?.FileUpload(_env.WebRootPath, "logos");
                result = await _repository.AddAsync(logo);
            }
            else
            {
                if (dto.HeaderLogo != null)
                {
                    FileManager.FileDelete(_env.WebRootPath, logo?.HeaderLogo ?? "");
                    logo.HeaderLogo = await dto.HeaderLogo.FileUpload(_env.WebRootPath, "logos");
                }

                if (dto.FooterLogo != null)
                {
                    FileManager.FileDelete(_env.WebRootPath, logo?.FooterLogo ?? "");
                    logo.FooterLogo = await dto.FooterLogo.FileUpload(_env.WebRootPath, "logos");
                }

                if (dto.Favicon != null)
                {
                    FileManager.FileDelete(_env.WebRootPath, logo?.Favicon ?? "");
                    logo.Favicon = await dto.Favicon.FileUpload(_env.WebRootPath, "logos");
                }
                result = await _repository.UpdateAsync(logo);
            }


            return result;
        }

        public async Task<LogoGetDTO?> GetFirst()
        {
            Logo logo = await _repository.GetQuery().FirstOrDefaultAsync();

            return logo == null ? null : _mapper.Map<LogoGetDTO>(logo);
        }
    }
}
