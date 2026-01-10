
using CB.Application.DTOs.LicensingProcessCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class LicensingProcessCaptionMappingProfile : MappingProfile
    {
        public LicensingProcessCaptionMappingProfile()
        {
            CreateMap<LicensingProcessCaption, LicensingProcessCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<LicensingProcessCaptionPostDTO, LicensingProcessCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
