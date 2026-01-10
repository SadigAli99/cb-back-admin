
using CB.Application.DTOs.About;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class AboutMappingProfile : MappingProfile
    {
        public AboutMappingProfile()
        {
            CreateMap<About, AboutGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<AboutPostDTO, About>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
