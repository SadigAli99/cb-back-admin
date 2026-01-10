
using CB.Application.DTOs.FinancialStabilityReportCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class FinancialStabilityReportCaptionMappingProfile : MappingProfile
    {
        public FinancialStabilityReportCaptionMappingProfile()
        {
            CreateMap<FinancialStabilityReportCaption, FinancialStabilityReportCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<FinancialStabilityReportCaptionPostDTO, FinancialStabilityReportCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
