
using CB.Application.DTOs.MeasAbout;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MeasAboutMappingProfile : MappingProfile
    {
        public MeasAboutMappingProfile()
        {
            CreateMap<MeasAbout, MeasAboutGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<MeasAboutPostDTO, MeasAbout>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
