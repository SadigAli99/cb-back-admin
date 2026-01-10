
using CB.Application.DTOs.FinancingActivityCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class FinancingActivityCaptionMappingProfile : MappingProfile
    {
        public FinancingActivityCaptionMappingProfile()
        {
            CreateMap<FinancingActivityCaption, FinancingActivityCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<FinancingActivityCaptionPostDTO, FinancingActivityCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
