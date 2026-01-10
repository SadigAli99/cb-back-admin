
using CB.Application.DTOs.InformationIssuerCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InformationIssuerCaptionMappingProfile : MappingProfile
    {
        public InformationIssuerCaptionMappingProfile()
        {
            CreateMap<InformationIssuerCaption, InformationIssuerCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<InformationIssuerCaptionPostDTO, InformationIssuerCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
