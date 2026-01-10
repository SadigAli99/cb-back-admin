
using CB.Application.DTOs.CurrencyHistory;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CurrencyHistoryMappingProfile : MappingProfile
    {
        public CurrencyHistoryMappingProfile() : base()
        {
            CreateMap<CurrencyHistory, CurrencyHistoryGetDTO>()
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    );

            CreateMap<CurrencyHistoryCreateDTO, CurrencyHistory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<CurrencyHistoryEditDTO, CurrencyHistory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
