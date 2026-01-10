
using CB.Application.DTOs.DigitalPortalCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class DigitalPortalCaptionMappingProfile : MappingProfile
    {
        public DigitalPortalCaptionMappingProfile()
        {
            CreateMap<DigitalPortalCaption, DigitalPortalCaptionGetDTO>()
                .ForMember(dest => dest.Titles, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Title
                        )
                    )
                )
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<DigitalPortalCaptionPostDTO, DigitalPortalCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
