
using CB.Application.DTOs.CoinMoneySignCharacteristic;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CoinMoneySignCharacteristicMappingProfile : MappingProfile
    {
        public CoinMoneySignCharacteristicMappingProfile() : base()
        {
            CreateMap<MoneySignCharacteristic, CoinMoneySignCharacteristicGetDTO>()
                .ForMember(dest => dest.MoneySignHistoryTitle, src => src.Ignore())
                .ForMember(dest => dest.Labels, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Label
                            )
                        )
                    )
                .ForMember(dest => dest.Values, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Value
                            )
                        )
                    );

            CreateMap<CoinMoneySignCharacteristicCreateDTO, MoneySignCharacteristic>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<CoinMoneySignCharacteristicEditDTO, MoneySignCharacteristic>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
