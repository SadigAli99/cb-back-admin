using AutoMapper;
using CB.Application.DTOs.Logo;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class LogoService : ILogoService
    {
        private readonly IGenericRepository<Logo> _repository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public LogoService(
            IMapper mapper,
            IGenericRepository<Logo> repository,
            IFileService fileService
        )
        {
            _mapper = mapper;
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<bool> CreateOrUpdate(LogoPostDTO dto)
        {
            Logo? logo = await _repository.GetQuery().FirstOrDefaultAsync();

            bool result;

            if (logo is null)
            {
                logo = new Logo();
                logo.HeaderLogo = await _fileService.UploadAsync(dto.HeaderLogo, "logos");
                logo.FooterLogo = await _fileService.UploadAsync(dto.FooterLogo, "logos");
                logo.Favicon = await _fileService.UploadAsync(dto.Favicon, "logos");
                result = await _repository.AddAsync(logo);
            }
            else
            {
                if (dto.HeaderLogo != null)
                {
                    _fileService.Delete(logo.HeaderLogo ?? "");
                    logo.HeaderLogo = await _fileService.UploadAsync(dto.HeaderLogo, "logos");
                }

                if (dto.FooterLogo != null)
                {
                    _fileService.Delete(logo.FooterLogo ?? "");
                    logo.FooterLogo = await _fileService.UploadAsync(dto.FooterLogo, "logos");
                }

                if (dto.Favicon != null)
                {
                    _fileService.Delete(logo.Favicon ?? "");
                    logo.Favicon = await _fileService.UploadAsync(dto.Favicon, "logos");
                }
                result = await _repository.UpdateAsync(logo);
            }


            return result;
        }

        public async Task<LogoGetDTO?> GetFirst()
        {
            Logo? logo = await _repository.GetQuery().FirstOrDefaultAsync();

            return logo == null ? null : _mapper.Map<LogoGetDTO>(logo);
        }
    }
}
