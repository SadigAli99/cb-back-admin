

using CB.Application.DTOs.CapitalMarketMinisterAct;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CapitalMarketMinisterActMappingProfile : MappingProfile
    {
        public CapitalMarketMinisterActMappingProfile() : base()
        {
            CreateMap<CapitalMarketMinisterAct, CapitalMarketMinisterActGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<CapitalMarketMinisterAct, CapitalMarketMinisterActGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<CapitalMarketMinisterActCreateDTO, CapitalMarketMinisterAct>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<CapitalMarketMinisterActEditDTO, CapitalMarketMinisterAct>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
