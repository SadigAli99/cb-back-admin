

using CB.Application.DTOs.CurrencyExchange;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CurrencyExchangeMappingProfile : MappingProfile
    {
        public CurrencyExchangeMappingProfile() : base()
        {
            CreateMap<CurrencyExchange, CurrencyExchangeGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<CurrencyExchange, CurrencyExchangeGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<CurrencyExchangeCreateDTO, CurrencyExchange>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<CurrencyExchangeEditDTO, CurrencyExchange>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
