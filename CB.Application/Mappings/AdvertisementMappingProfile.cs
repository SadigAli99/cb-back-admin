

using CB.Application.DTOs.Advertisement;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class AdvertisementMappingProfile : MappingProfile
    {
        public AdvertisementMappingProfile() : base()
        {
            CreateMap<Advertisement, AdvertisementGetDTO>()
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


            CreateMap<AdvertisementCreateDTO, Advertisement>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<AdvertisementEditDTO, Advertisement>()
                .ForMember(dest => dest.Translations, src => src.Ignore());
        }
    }
}
