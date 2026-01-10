
using CB.Application.DTOs.FinancialLiteracyCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class FinancialLiteracyCaptionMappingProfile : MappingProfile
    {
        public FinancialLiteracyCaptionMappingProfile()
        {
            CreateMap<FinancialLiteracyCaption, FinancialLiteracyCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<FinancialLiteracyCaptionPostDTO, FinancialLiteracyCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
