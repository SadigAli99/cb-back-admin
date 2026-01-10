
using CB.Application.DTOs.FaqVideo;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class FaqVideoMappingProfile : MappingProfile
    {
        public FaqVideoMappingProfile()
        {
            CreateMap<FaqVideo, FaqVideoGetDTO>()
                .ForMember(dest => dest.Titles, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Title
                        )
                    )
                );

            CreateMap<FaqVideoPostDTO, FaqVideo>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
