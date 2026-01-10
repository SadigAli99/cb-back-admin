

using CB.Application.DTOs.StockExchange;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class StockExchangeMappingProfile : MappingProfile
    {
        public StockExchangeMappingProfile() : base()
        {
            CreateMap<StockExchange, StockExchangeGetDTO>()
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.Descriptions, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Description
                    ));
                });


            CreateMap<StockExchangeCreateDTO, StockExchange>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<StockExchangeEditDTO, StockExchange>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
