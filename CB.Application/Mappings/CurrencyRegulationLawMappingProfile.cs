

using CB.Application.DTOs.CurrencyRegulationLaw;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CurrencyRegulationLawMappingProfile : MappingProfile
    {
        public CurrencyRegulationLawMappingProfile() : base()
        {
            CreateMap<CurrencyRegulationLaw, CurrencyRegulationLawGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<CurrencyRegulationLaw, CurrencyRegulationLawGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<CurrencyRegulationLawCreateDTO, CurrencyRegulationLaw>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<CurrencyRegulationLawEditDTO, CurrencyRegulationLaw>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
