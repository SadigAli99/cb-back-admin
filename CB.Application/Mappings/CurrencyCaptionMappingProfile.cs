
using CB.Application.DTOs.CurrencyCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CurrencyCaptionMappingProfile : MappingProfile
    {
        public CurrencyCaptionMappingProfile()
        {
            CreateMap<CurrencyCaption, CurrencyCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<CurrencyCaptionPostDTO, CurrencyCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
