
using CB.Application.DTOs.BankInvestmentCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class BankInvestmentCaptionMappingProfile : MappingProfile
    {
        public BankInvestmentCaptionMappingProfile()
        {
            CreateMap<BankInvestmentCaption, BankInvestmentCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<BankInvestmentCaptionPostDTO, BankInvestmentCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
