
using CB.Application.DTOs.DigitalPaymentInfograhicCategory;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class DigitalPaymentInfograhicCategoryMappingProfile : MappingProfile
    {
        public DigitalPaymentInfograhicCategoryMappingProfile() : base()
        {
            CreateMap<DigitalPaymentInfograhicCategory, DigitalPaymentInfograhicCategoryGetDTO>()
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    );

            CreateMap<DigitalPaymentInfograhicCategoryCreateDTO, DigitalPaymentInfograhicCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<DigitalPaymentInfograhicCategoryEditDTO, DigitalPaymentInfograhicCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
