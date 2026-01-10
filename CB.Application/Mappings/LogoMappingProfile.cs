
using CB.Application.DTOs.Logo;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class LogoMappingProfile : MappingProfile
    {
        public LogoMappingProfile()
        {
            CreateMap<Logo, LogoGetDTO>()
                .ForMember(dest => dest.HeaderLogo, opt => opt.MapFrom<GenericResolver<Logo, LogoGetDTO>>())
                .ForMember(dest => dest.FooterLogo, opt => opt.MapFrom<GenericResolver<Logo, LogoGetDTO>>())
                .ForMember(dest => dest.Favicon, opt => opt.MapFrom<GenericResolver<Logo, LogoGetDTO>>());


            CreateMap<LogoPostDTO, Logo>();
        }
    }
}
