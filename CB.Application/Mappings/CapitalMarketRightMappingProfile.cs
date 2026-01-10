

using CB.Application.DTOs.CapitalMarketRight;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CapitalMarketRightMappingProfile : MappingProfile
    {
        public CapitalMarketRightMappingProfile() : base()
        {
            CreateMap<CapitalMarketRight, CapitalMarketRightGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<CapitalMarketRight, CapitalMarketRightGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<CapitalMarketRightCreateDTO, CapitalMarketRight>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<CapitalMarketRightEditDTO, CapitalMarketRight>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
