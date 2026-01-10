

using CB.Application.DTOs.DigitalPaymentReview;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class DigitalPaymentReviewMappingProfile : MappingProfile
    {
        public DigitalPaymentReviewMappingProfile() : base()
        {
            CreateMap<DigitalPaymentReview, DigitalPaymentReviewGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<DigitalPaymentReview, DigitalPaymentReviewGetDTO>>())
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


            CreateMap<DigitalPaymentReviewCreateDTO, DigitalPaymentReview>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<DigitalPaymentReviewEditDTO, DigitalPaymentReview>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
