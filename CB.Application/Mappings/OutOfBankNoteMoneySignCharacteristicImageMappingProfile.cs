

using CB.Application.DTOs.OutOfBankNoteMoneySignCharacteristicImage;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class OutOfBankNoteMoneySignCharacteristicImageMappingProfile : MappingProfile
    {
        public OutOfBankNoteMoneySignCharacteristicImageMappingProfile() : base()
        {
            CreateMap<MoneySignCharacteristicImage, OutOfBankNoteMoneySignCharacteristicImageGetDTO>()
                .ForMember(dest => dest.FrontImage, opt => opt.Ignore())
                .ForMember(dest => dest.BackImage, opt => opt.Ignore());


            CreateMap<OutOfBankNoteMoneySignCharacteristicImageCreateDTO, MoneySignCharacteristicImage>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<OutOfBankNoteMoneySignCharacteristicImageEditDTO, MoneySignCharacteristicImage>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
