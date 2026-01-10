

using CB.Application.DTOs.ReviewApplicationVideo;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ReviewApplicationVideoMappingProfile : MappingProfile
    {
        public ReviewApplicationVideoMappingProfile() : base()
        {
            CreateMap<ReviewApplicationVideo, ReviewApplicationVideoGetDTO>()
                .ForMember(dest => dest.ReviewApplicationTitle, src => src.Ignore())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<ReviewApplicationVideoCreateDTO, ReviewApplicationVideo>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<ReviewApplicationVideoEditDTO, ReviewApplicationVideo>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
