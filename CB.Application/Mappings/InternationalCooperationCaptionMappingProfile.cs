
using CB.Application.DTOs.InternationalCooperationCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InternationalCooperationCaptionMappingProfile : MappingProfile
    {
        public InternationalCooperationCaptionMappingProfile()
        {
            CreateMap<InternationalCooperationCaption, InternationalCooperationCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<InternationalCooperationCaptionPostDTO, InternationalCooperationCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
