
using CB.Application.DTOs.MediaCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MediaCaptionMappingProfile : MappingProfile
    {
        public MediaCaptionMappingProfile()
        {
            CreateMap<MediaCaption, MediaCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<MediaCaptionPostDTO, MediaCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
