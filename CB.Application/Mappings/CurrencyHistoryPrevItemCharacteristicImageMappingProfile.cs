

using CB.Application.DTOs.CurrencyHistoryPrevItemCharacteristicImage;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CurrencyHistoryPrevItemCharacteristicImageMappingProfile : MappingProfile
    {
        public CurrencyHistoryPrevItemCharacteristicImageMappingProfile() : base()
        {
            CreateMap<CurrencyHistoryPrevItemCharacteristicImage, CurrencyHistoryPrevItemCharacteristicImageGetDTO>()
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


            CreateMap<CurrencyHistoryPrevItemCharacteristicImageCreateDTO, CurrencyHistoryPrevItemCharacteristicImage>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<CurrencyHistoryPrevItemCharacteristicImageEditDTO, CurrencyHistoryPrevItemCharacteristicImage>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
