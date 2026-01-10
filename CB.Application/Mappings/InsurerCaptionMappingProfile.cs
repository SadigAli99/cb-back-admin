
using CB.Application.DTOs.InsurerCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InsurerCaptionMappingProfile : MappingProfile
    {
        public InsurerCaptionMappingProfile()
        {
            CreateMap<InsurerCaption, InsurerCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<InsurerCaptionPostDTO, InsurerCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
