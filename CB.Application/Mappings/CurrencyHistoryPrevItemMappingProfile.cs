

using CB.Application.DTOs.CurrencyHistoryPrevItem;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CurrencyHistoryPrevItemMappingProfile : MappingProfile
    {
        public CurrencyHistoryPrevItemMappingProfile() : base()
        {
            CreateMap<CurrencyHistoryPrevItem, CurrencyHistoryPrevItemGetDTO>()
                .ForMember(dest => dest.CurrencyHistoryPrevTitle, src => src.Ignore())
                .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    )
                .ForMember(dest => dest.Descriptions, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Description
                            )
                        )
                    );


            CreateMap<CurrencyHistoryPrevItemCreateDTO, CurrencyHistoryPrevItem>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<CurrencyHistoryPrevItemEditDTO, CurrencyHistoryPrevItem>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
