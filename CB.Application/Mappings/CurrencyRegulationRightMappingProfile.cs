

using CB.Application.DTOs.CurrencyRegulationRight;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CurrencyRegulationRightMappingProfile : MappingProfile
    {
        public CurrencyRegulationRightMappingProfile() : base()
        {
            CreateMap<CurrencyRegulationRight, CurrencyRegulationRightGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<CurrencyRegulationRight, CurrencyRegulationRightGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<CurrencyRegulationRightCreateDTO, CurrencyRegulationRight>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<CurrencyRegulationRightEditDTO, CurrencyRegulationRight>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
