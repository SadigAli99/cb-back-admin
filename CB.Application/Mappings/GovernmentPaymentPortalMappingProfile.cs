
using CB.Application.DTOs.GovernmentPaymentPortal;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class GovernmentPaymentPortalMappingProfile : MappingProfile
    {
        public GovernmentPaymentPortalMappingProfile()
        {
            CreateMap<GovernmentPaymentPortal, GovernmentPaymentPortalGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<GovernmentPaymentPortalPostDTO, GovernmentPaymentPortal>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
