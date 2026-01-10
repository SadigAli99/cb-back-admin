
using CB.Application.DTOs.CreditUnionCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CreditUnionCaptionMappingProfile : MappingProfile
    {
        public CreditUnionCaptionMappingProfile()
        {
            CreateMap<CreditUnionCaption, CreditUnionCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<CreditUnionCaptionPostDTO, CreditUnionCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
