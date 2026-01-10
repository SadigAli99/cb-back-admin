

using CB.Application.DTOs.CoinMoneySignCharacteristicImage;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CoinMoneySignCharacteristicImageMappingProfile : MappingProfile
    {
        public CoinMoneySignCharacteristicImageMappingProfile() : base()
        {
            CreateMap<MoneySignCharacteristicImage, CoinMoneySignCharacteristicImageGetDTO>()
                .ForMember(dest => dest.FrontImage, opt => opt.Ignore())
                .ForMember(dest=>dest.BackImage, opt=>opt.Ignore());


            CreateMap<CoinMoneySignCharacteristicImageCreateDTO, MoneySignCharacteristicImage>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<CoinMoneySignCharacteristicImageEditDTO, MoneySignCharacteristicImage>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
