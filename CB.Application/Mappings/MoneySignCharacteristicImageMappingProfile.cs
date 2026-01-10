

using CB.Application.DTOs.MoneySignCharacteristicImage;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MoneySignCharacteristicImageMappingProfile : MappingProfile
    {
        public MoneySignCharacteristicImageMappingProfile() : base()
        {
            CreateMap<MoneySignCharacteristicImage, MoneySignCharacteristicImageGetDTO>()
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


            CreateMap<MoneySignCharacteristicImageCreateDTO, MoneySignCharacteristicImage>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<MoneySignCharacteristicImageEditDTO, MoneySignCharacteristicImage>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
