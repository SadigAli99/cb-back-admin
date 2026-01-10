

using CB.Application.DTOs.MonetaryPolicyReview;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MonetaryPolicyReviewMappingProfile : MappingProfile
    {
        public MonetaryPolicyReviewMappingProfile() : base()
        {
            CreateMap<MonetaryPolicyReview, MonetaryPolicyReviewGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<MonetaryPolicyReview, MonetaryPolicyReviewGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.CoverTitles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.CoverTitle
                    ));
                });


            CreateMap<MonetaryPolicyReviewCreateDTO, MonetaryPolicyReview>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<MonetaryPolicyReviewEditDTO, MonetaryPolicyReview>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
