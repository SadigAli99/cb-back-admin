
using CB.Application.DTOs.OutOfCoinMoneySignCharacteristic;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class OutOfCoinMoneySignCharacteristicMappingProfile : MappingProfile
    {
        public OutOfCoinMoneySignCharacteristicMappingProfile() : base()
        {
            CreateMap<MoneySignCharacteristic, OutOfCoinMoneySignCharacteristicGetDTO>()
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

            CreateMap<OutOfCoinMoneySignCharacteristicCreateDTO, MoneySignCharacteristic>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<OutOfCoinMoneySignCharacteristicEditDTO, MoneySignCharacteristic>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
