

using CB.Application.DTOs.CurrencyHistoryNext;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CurrencyHistoryNextMappingProfile : MappingProfile
    {
        public CurrencyHistoryNextMappingProfile() : base()
        {
            CreateMap<CurrencyHistoryNext, CurrencyHistoryNextGetDTO>()
                .ForMember(dest => dest.CurrencyHistoryTitle, src => src.Ignore())
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


            CreateMap<CurrencyHistoryNextCreateDTO, CurrencyHistoryNext>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<CurrencyHistoryNextEditDTO, CurrencyHistoryNext>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
