
using CB.Application.DTOs.CurrencyRegulationRightCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CurrencyRegulationRightCaptionMappingProfile : MappingProfile
    {
        public CurrencyRegulationRightCaptionMappingProfile()
        {
            CreateMap<CurrencyRegulationRightCaption, CurrencyRegulationRightCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<CurrencyRegulationRightCaptionPostDTO, CurrencyRegulationRightCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
