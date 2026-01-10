
using CB.Application.DTOs.DevelopmentArticle;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class DevelopmentArticleMappingProfile : MappingProfile
    {
        public DevelopmentArticleMappingProfile()
        {
            CreateMap<DevelopmentArticle, DevelopmentArticleGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<DevelopmentArticlePostDTO, DevelopmentArticle>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
