

using CB.Application.DTOs.Video;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class VideoMappingProfile : MappingProfile
    {
        public VideoMappingProfile() : base()
        {
            CreateMap<Video, VideoGetDTO>()
                .ForMember(dest=>dest.Image,src=>src.MapFrom<GenericResolver<Video,VideoGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<VideoCreateDTO, Video>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<VideoEditDTO, Video>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
