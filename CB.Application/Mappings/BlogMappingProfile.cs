

using CB.Application.DTOs.Blog;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class BlogMappingProfile : MappingProfile
    {
        public BlogMappingProfile() : base()
        {
            CreateMap<Blog, BlogGetDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<Blog, BlogGetDTO>>())
                .ForMember(dest => dest.Images, src => src.Ignore())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.Slugs, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Slug
                    ));
                })
                .ForMember(dest => dest.ImageTitles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.ImageTitle
                    ));
                })
                .ForMember(dest => dest.ImageAlts, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.ImageAlt
                    ));
                })
                .ForMember(dest => dest.MetaTitles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.MetaTitle
                    ));
                })
                .ForMember(dest => dest.MetaDescriptions, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.MetaDescription
                    ));
                })
                .ForMember(dest => dest.MetaKeywords, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.MetaKeyword
                    ));
                })
                .ForMember(dest => dest.ShortDescriptions, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.ShortDescription
                    ));
                })
                .ForMember(dest => dest.Descriptions, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Description
                    ));
                });


            CreateMap<BlogCreateDTO, Blog>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.Images, src => src.Ignore());

            CreateMap<BlogEditDTO, Blog>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.Images, src => src.Ignore());

            CreateMap<BlogImage, BlogImageDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<BlogImage, BlogImageDTO>>());

        }
    }
}
