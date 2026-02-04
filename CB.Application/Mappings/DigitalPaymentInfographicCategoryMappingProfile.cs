
using CB.Application.DTOs.DigitalPaymentInfographicCategory;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class DigitalPaymentInfographicCategoryMappingProfile : MappingProfile
    {
        public DigitalPaymentInfographicCategoryMappingProfile() : base()
        {
            CreateMap<DigitalPaymentInfographicCategory, DigitalPaymentInfographicCategoryGetDTO>()
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    );

            CreateMap<DigitalPaymentInfographicCategoryCreateDTO, DigitalPaymentInfographicCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<DigitalPaymentInfographicCategoryEditDTO, DigitalPaymentInfographicCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
