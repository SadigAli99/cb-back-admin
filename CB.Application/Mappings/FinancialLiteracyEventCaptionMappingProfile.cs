
using CB.Application.DTOs.FinancialLiteracyEventCaption;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class FinancialLiteracyEventCaptionMappingProfile : MappingProfile
    {
        public FinancialLiteracyEventCaptionMappingProfile()
        {
            CreateMap<FinancialLiteracyEventCaption, FinancialLiteracyEventCaptionGetDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<FinancialLiteracyEventCaption, FinancialLiteracyEventCaptionGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Title
                        )
                    )
                );

            CreateMap<FinancialLiteracyEventCaptionPostDTO, FinancialLiteracyEventCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
