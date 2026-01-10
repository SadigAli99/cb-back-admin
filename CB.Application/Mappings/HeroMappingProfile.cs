
using CB.Application.DTOs.Hero;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class HeroMappingProfile : MappingProfile
    {
        public HeroMappingProfile()
        {
            CreateMap<Hero, HeroGetDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<Hero, HeroGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Title
                        )
                    )
                );

            CreateMap<HeroPostDTO, Hero>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
