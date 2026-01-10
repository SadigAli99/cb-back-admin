
using CB.Application.DTOs.InvestmentCompanyCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InvestmentCompanyCaptionMappingProfile : MappingProfile
    {
        public InvestmentCompanyCaptionMappingProfile()
        {
            CreateMap<InvestmentCompanyCaption, InvestmentCompanyCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<InvestmentCompanyCaptionPostDTO, InvestmentCompanyCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
