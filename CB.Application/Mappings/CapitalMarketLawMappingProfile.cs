

using CB.Application.DTOs.CapitalMarketLaw;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CapitalMarketLawMappingProfile : MappingProfile
    {
        public CapitalMarketLawMappingProfile() : base()
        {
            CreateMap<CapitalMarketLaw, CapitalMarketLawGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<CapitalMarketLaw, CapitalMarketLawGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<CapitalMarketLawCreateDTO, CapitalMarketLaw>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<CapitalMarketLawEditDTO, CapitalMarketLaw>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
