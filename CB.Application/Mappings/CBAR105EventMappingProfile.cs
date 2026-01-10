
using CB.Application.DTOs.CBAR105Event;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CBAR105EventMappingProfile : MappingProfile
    {
        public CBAR105EventMappingProfile()
        {
            CreateMap<CBAR105Event, CBAR105EventGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<CBAR105EventPostDTO, CBAR105Event>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ForMember(dest => dest.Images, src => src.Ignore())
                .ReverseMap();

            CreateMap<CBAR105EventImage, CBAR105EventImageDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<CBAR105EventImage, CBAR105EventImageDTO>>());
        }
    }
}
