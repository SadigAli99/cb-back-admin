

using CB.Application.DTOs.CurrencyHistoryNextItem;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CurrencyHistoryNextItemMappingProfile : MappingProfile
    {
        public CurrencyHistoryNextItemMappingProfile() : base()
        {
            CreateMap<CurrencyHistoryNextItem, CurrencyHistoryNextItemGetDTO>()
                .ForMember(dest => dest.FrontImage, opt => opt.Ignore())
                .ForMember(dest=>dest.BackImage, opt=>opt.Ignore());


            CreateMap<CurrencyHistoryNextItemCreateDTO, CurrencyHistoryNextItem>();

            CreateMap<CurrencyHistoryNextItemEditDTO, CurrencyHistoryNextItem>();

        }
    }
}
