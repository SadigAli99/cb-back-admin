

using CB.Application.DTOs.CurrencyHistoryPrev;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CurrencyHistoryPrevMappingProfile : MappingProfile
    {
        public CurrencyHistoryPrevMappingProfile() : base()
        {
            CreateMap<CurrencyHistoryPrev, CurrencyHistoryPrevGetDTO>()
                .ForMember(dest => dest.CurrencyHistoryTitle, src => src.Ignore())
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<CurrencyHistoryPrev, CurrencyHistoryPrevGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.SubTitles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<CurrencyHistoryPrevCreateDTO, CurrencyHistoryPrev>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<CurrencyHistoryPrevEditDTO, CurrencyHistoryPrev>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
