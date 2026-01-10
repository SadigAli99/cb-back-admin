
using CB.Application.DTOs.Social;
using CB.Core.Entities;
using CB.Application.Mappings.Resolvers;

namespace CB.Application.Mappings
{
    public class SocialMappingProfile : MappingProfile
    {
        public SocialMappingProfile()
        {
            CreateMap<Social, SocialGetDTO>()
                .ForMember(dest => dest.Icon, opt => opt.MapFrom<GenericResolver<Social, SocialGetDTO>>());
            CreateMap<SocialCreateDTO, Social>();
            CreateMap<SocialCreateDTO, SocialEditDTO>()
                .ForMember(dest => dest.File, src => src.Ignore());
            CreateMap<SocialEditDTO, Social>().ReverseMap();
        }
    }
}
