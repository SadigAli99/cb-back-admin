

using CB.Application.DTOs.DigitalPaymentInfographic;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class DigitalPaymentInfographicMappingProfile : MappingProfile
    {
        public DigitalPaymentInfographicMappingProfile() : base()
        {
            CreateMap<DigitalPaymentInfographic, DigitalPaymentInfographicGetDTO>()
                .ForMember(dest => dest.DigitalPaymentInfographicCategoryTitle, src => src.Ignore())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<DigitalPaymentInfographicCreateDTO, DigitalPaymentInfographic>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<DigitalPaymentInfographicEditDTO, DigitalPaymentInfographic>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
