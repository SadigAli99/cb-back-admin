

using CB.Application.DTOs.CybersecurityStrategy;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CybersecurityStrategyMappingProfile : MappingProfile
    {
        public CybersecurityStrategyMappingProfile() : base()
        {
            CreateMap<CybersecurityStrategy, CybersecurityStrategyGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<CybersecurityStrategy, CybersecurityStrategyGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.CoverTitles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.CoverTitle
                    ));
                });


            CreateMap<CybersecurityStrategyCreateDTO, CybersecurityStrategy>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<CybersecurityStrategyEditDTO, CybersecurityStrategy>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
