
using CB.Application.DTOs.FinancialLiteracyPortalCaption;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class FinancialLiteracyPortalCaptionMappingProfile : MappingProfile
    {
        public FinancialLiteracyPortalCaptionMappingProfile()
        {
            CreateMap<FinancialLiteracyPortalCaption, FinancialLiteracyPortalCaptionGetDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<FinancialLiteracyPortalCaption, FinancialLiteracyPortalCaptionGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Title
                        )
                    )
                );

            CreateMap<FinancialLiteracyPortalCaptionPostDTO, FinancialLiteracyPortalCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
