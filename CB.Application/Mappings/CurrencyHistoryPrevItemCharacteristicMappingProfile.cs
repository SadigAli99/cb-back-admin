
using CB.Application.DTOs.CurrencyHistoryPrevItemCharacteristic;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CurrencyHistoryPrevItemCharacteristicMappingProfile : MappingProfile
    {
        public CurrencyHistoryPrevItemCharacteristicMappingProfile() : base()
        {
            CreateMap<CurrencyHistoryPrevItemCharacteristic, CurrencyHistoryPrevItemCharacteristicGetDTO>()
                .ForMember(dest => dest.CurrencyHistoryPrevItemTitle, src => src.Ignore())
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

            CreateMap<CurrencyHistoryPrevItemCharacteristicCreateDTO, CurrencyHistoryPrevItemCharacteristic>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<CurrencyHistoryPrevItemCharacteristicEditDTO, CurrencyHistoryPrevItemCharacteristic>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
