
using CB.Application.DTOs.MoneySignCharacteristic;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MoneySignCharacteristicMappingProfile : MappingProfile
    {
        public MoneySignCharacteristicMappingProfile() : base()
        {
            CreateMap<MoneySignCharacteristic, MoneySignCharacteristicGetDTO>()
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

            CreateMap<MoneySignCharacteristicCreateDTO, MoneySignCharacteristic>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<MoneySignCharacteristicEditDTO, MoneySignCharacteristic>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
