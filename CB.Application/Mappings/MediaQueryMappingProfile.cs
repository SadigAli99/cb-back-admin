
using CB.Application.DTOs.MediaQuery;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MediaQueryMappingProfile : MappingProfile
    {
        public MediaQueryMappingProfile()
        {
            CreateMap<MediaQuery, MediaQueryGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<MediaQueryPostDTO, MediaQuery>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
