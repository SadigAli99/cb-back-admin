
using CB.Application.DTOs.PolicyAnalysis;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class PolicyAnalysisMappingProfile : MappingProfile
    {
        public PolicyAnalysisMappingProfile()
        {
            CreateMap<PolicyAnalysis, PolicyAnalysisGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<PolicyAnalysisPostDTO, PolicyAnalysis>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
