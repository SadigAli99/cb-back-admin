
using CB.Application.DTOs.StateProgramCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class StateProgramCaptionMappingProfile : MappingProfile
    {
        public StateProgramCaptionMappingProfile()
        {
            CreateMap<StateProgramCaption, StateProgramCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<StateProgramCaptionPostDTO, StateProgramCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
