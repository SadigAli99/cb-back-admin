
using CB.Application.DTOs.InvestmentFundCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InvestmentFundCaptionMappingProfile : MappingProfile
    {
        public InvestmentFundCaptionMappingProfile()
        {
            CreateMap<InvestmentFundCaption, InvestmentFundCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<InvestmentFundCaptionPostDTO, InvestmentFundCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
