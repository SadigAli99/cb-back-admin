

using CB.Application.DTOs.OutOfCoinMoneySign;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class OutOfCoinMoneySignMappingProfile : MappingProfile
    {
        public OutOfCoinMoneySignMappingProfile() : base()
        {
            CreateMap<MoneySign, OutOfCoinMoneySignGetDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<MoneySign, OutOfCoinMoneySignGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<OutOfCoinMoneySignCreateDTO, MoneySign>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<OutOfCoinMoneySignEditDTO, MoneySign>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
