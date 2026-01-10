

using CB.Application.DTOs.CapitalMarketPresidentAct;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CapitalMarketPresidentActMappingProfile : MappingProfile
    {
        public CapitalMarketPresidentActMappingProfile() : base()
        {
            CreateMap<CapitalMarketPresidentAct, CapitalMarketPresidentActGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<CapitalMarketPresidentAct, CapitalMarketPresidentActGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<CapitalMarketPresidentActCreateDTO, CapitalMarketPresidentAct>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<CapitalMarketPresidentActEditDTO, CapitalMarketPresidentAct>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
