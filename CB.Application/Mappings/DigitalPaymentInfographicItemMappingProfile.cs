

using CB.Application.DTOs.DigitalPaymentInfographicItem;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class DigitalPaymentInfographicItemMappingProfile : MappingProfile
    {
        public DigitalPaymentInfographicItemMappingProfile() : base()
        {
            CreateMap<DigitalPaymentInfographicItem, DigitalPaymentInfographicItemGetDTO>()
                .ForMember(dest => dest.DigitalPaymentInfographicTitle, src => src.Ignore());


            CreateMap<DigitalPaymentInfographicItemCreateDTO, DigitalPaymentInfographicItem>();

            CreateMap<DigitalPaymentInfographicItemEditDTO, DigitalPaymentInfographicItem>();

        }
    }
}
