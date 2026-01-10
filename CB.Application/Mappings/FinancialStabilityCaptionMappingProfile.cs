
using CB.Application.DTOs.FinancialStabilityCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class FinancialStabilityCaptionMappingProfile : MappingProfile
    {
        public FinancialStabilityCaptionMappingProfile()
        {
            CreateMap<FinancialStabilityCaption, FinancialStabilityCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<FinancialStabilityCaptionPostDTO, FinancialStabilityCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
