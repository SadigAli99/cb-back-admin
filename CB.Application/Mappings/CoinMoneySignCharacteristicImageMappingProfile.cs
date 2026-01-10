

using CB.Application.DTOs.OutOfCoinMoneySignCharacteristicImage;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class OutOfCoinMoneySignCharacteristicImageMappingProfile : MappingProfile
    {
        public OutOfCoinMoneySignCharacteristicImageMappingProfile() : base()
        {
            CreateMap<MoneySignCharacteristicImage, OutOfCoinMoneySignCharacteristicImageGetDTO>()
                .ForMember(dest => dest.FrontImage, opt => opt.Ignore())
                .ForMember(dest=>dest.BackImage, opt=>opt.Ignore())
                .ForMember(dest => dest.Colors, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Color
                    ));
                })
                .ForMember(dest => dest.Sizes, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Size
                    ));
                });


            CreateMap<OutOfCoinMoneySignCharacteristicImageCreateDTO, MoneySignCharacteristicImage>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<OutOfCoinMoneySignCharacteristicImageEditDTO, MoneySignCharacteristicImage>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
