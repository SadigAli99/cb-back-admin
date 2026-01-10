

using CB.Application.DTOs.MacroEconomy;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MacroEconomyMappingProfile : MappingProfile
    {
        public MacroEconomyMappingProfile() : base()
        {
            CreateMap<MacroEconomy, MacroEconomyGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<MacroEconomy, MacroEconomyGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<MacroEconomyCreateDTO, MacroEconomy>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<MacroEconomyEditDTO, MacroEconomy>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
