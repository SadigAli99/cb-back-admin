
using CB.Application.DTOs.StaffArticle;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class StaffArticleMappingProfile : MappingProfile
    {
        public StaffArticleMappingProfile() : base()
        {
            CreateMap<StaffArticle, StaffArticleGetDTO>()
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    )
                    .ForMember(dest => dest.Slugs, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Slug
                            )
                        )
                    )
                    .ForMember(dest => dest.SubTitles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.SubTitle
                            )
                        )
                    )
                    .ForMember(dest => dest.Descriptions, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Description
                            )
                        )
                    );

            CreateMap<StaffArticleCreateDTO, StaffArticle>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<StaffArticleEditDTO, StaffArticle>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
