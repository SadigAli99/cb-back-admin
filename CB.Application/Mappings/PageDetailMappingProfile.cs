using CB.Application.DTOs.PageDetail;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class PageDetailMappingProfile : MappingProfile
    {
        public PageDetailMappingProfile() : base()
        {
            CreateMap<PageDetail, PageDetailGetDTO>()
                    .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<PageDetail, PageDetailGetDTO>>())
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


            CreateMap<PageDetailCreateDTO, PageDetail>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<PageDetailEditDTO, PageDetail>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

        }
    }
}
