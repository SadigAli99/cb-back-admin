

using CB.Application.DTOs.ReviewApplicationLink;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ReviewApplicationLinkMappingProfile : MappingProfile
    {
        public ReviewApplicationLinkMappingProfile() : base()
        {
            CreateMap<ReviewApplicationLink, ReviewApplicationLinkGetDTO>()
                .ForMember(dest => dest.ReviewApplicationTitle, src => src.Ignore())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<ReviewApplicationLinkCreateDTO, ReviewApplicationLink>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<ReviewApplicationLinkEditDTO, ReviewApplicationLink>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
