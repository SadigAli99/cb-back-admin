
using CB.Application.DTOs.MissionCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MissionCaptionMappingProfile : MappingProfile
    {
        public MissionCaptionMappingProfile()
        {
            CreateMap<MissionCaption, MissionCaptionGetDTO>()
                .ForMember(dest => dest.Titles, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Title
                        )
                    )
                )
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<MissionCaptionPostDTO, MissionCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
