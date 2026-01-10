

using CB.Application.DTOs.DigitalPaymentInfograhicItem;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class DigitalPaymentInfograhicItemMappingProfile : MappingProfile
    {
        public DigitalPaymentInfograhicItemMappingProfile() : base()
        {
            CreateMap<DigitalPaymentInfograhicItem, DigitalPaymentInfograhicItemGetDTO>()
                .ForMember(dest => dest.DigitalPaymentInfograhicTitle, src => src.Ignore());


            CreateMap<DigitalPaymentInfograhicItemCreateDTO, DigitalPaymentInfograhicItem>();

            CreateMap<DigitalPaymentInfograhicItemEditDTO, DigitalPaymentInfograhicItem>();

        }
    }
}
