
using CB.Application.DTOs.StaffArticleCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class StaffArticleCaptionMappingProfile : MappingProfile
    {
        public StaffArticleCaptionMappingProfile()
        {
            CreateMap<StaffArticleCaption, StaffArticleCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<StaffArticleCaptionPostDTO, StaffArticleCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
