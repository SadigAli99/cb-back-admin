
using CB.Application.DTOs.Page;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class PageMappingProfile : MappingProfile
    {
        public PageMappingProfile() : base()
        {
            CreateMap<Page, PageGetDTO>()
                    .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<Page, PageGetDTO>>())
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    )
                    .ForMember(dest => dest.Urls, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Url
                            )
                        )
                    )
                    .ForMember(dest => dest.MetaTitles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.MetaTitle
                            )
                        )
                    )
                    .ForMember(dest => dest.MetaDescriptions, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.MetaDescription
                            )
                        )
                    );


            CreateMap<PageCreateDTO, Page>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<PageEditDTO, Page>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
