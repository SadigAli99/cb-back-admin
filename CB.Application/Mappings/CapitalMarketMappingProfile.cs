

using CB.Application.DTOs.CapitalMarket;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CapitalMarketMappingProfile : MappingProfile
    {
        public CapitalMarketMappingProfile() : base()
        {
            CreateMap<CapitalMarket, CapitalMarketGetDTO>()
                .ForMember(dest => dest.CapitalMarketCategory, src => src.Ignore())
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<CapitalMarket, CapitalMarketGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<CapitalMarketCreateDTO, CapitalMarket>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<CapitalMarketEditDTO, CapitalMarket>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
