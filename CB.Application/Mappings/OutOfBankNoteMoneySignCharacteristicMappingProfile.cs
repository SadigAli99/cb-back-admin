
using CB.Application.DTOs.OutOfBankNoteMoneySignCharacteristic;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class OutOfBankNoteMoneySignCharacteristicMappingProfile : MappingProfile
    {
        public OutOfBankNoteMoneySignCharacteristicMappingProfile() : base()
        {
            CreateMap<MoneySignCharacteristic, OutOfBankNoteMoneySignCharacteristicGetDTO>()
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

            CreateMap<OutOfBankNoteMoneySignCharacteristicCreateDTO, MoneySignCharacteristic>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<OutOfBankNoteMoneySignCharacteristicEditDTO, MoneySignCharacteristic>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
