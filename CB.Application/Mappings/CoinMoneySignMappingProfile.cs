

using CB.Application.DTOs.CoinMoneySign;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CoinMoneySignMappingProfile : MappingProfile
    {
        public CoinMoneySignMappingProfile() : base()
        {
            CreateMap<MoneySign, CoinMoneySignGetDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<MoneySign, CoinMoneySignGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<CoinMoneySignCreateDTO, MoneySign>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<CoinMoneySignEditDTO, MoneySign>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
