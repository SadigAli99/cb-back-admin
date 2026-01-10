

using CB.Application.DTOs.NakhchivanBlog;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class NakhchivanBlogMappingProfile : MappingProfile
    {
        public NakhchivanBlogMappingProfile() : base()
        {
            CreateMap<NakhchivanBlog, NakhchivanBlogGetDTO>()
                .ForMember(dest => dest.Images, src => src.Ignore())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.Descriptions, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Description
                    ));
                });


            CreateMap<NakhchivanBlogCreateDTO, NakhchivanBlog>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.Images, src => src.Ignore());

            CreateMap<NakhchivanBlogEditDTO, NakhchivanBlog>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.Images, src => src.Ignore());

            CreateMap<NakhchivanBlogImage, NakhchivanBlogImageDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<NakhchivanBlogImage, NakhchivanBlogImageDTO>>());

        }
    }
}
