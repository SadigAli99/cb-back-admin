
using CB.Application.DTOs.PresidentDecree;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class PresidentDecreeMappingProfile : MappingProfile
    {
        public PresidentDecreeMappingProfile()
        {
            CreateMap<PresidentDecree, PresidentDecreeGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<PresidentDecreePostDTO, PresidentDecree>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
