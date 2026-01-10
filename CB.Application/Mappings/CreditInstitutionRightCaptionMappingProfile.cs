
using CB.Application.DTOs.CreditInstitutionRightCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CreditInstitutionRightCaptionMappingProfile : MappingProfile
    {
        public CreditInstitutionRightCaptionMappingProfile()
        {
            CreateMap<CreditInstitutionRightCaption, CreditInstitutionRightCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<CreditInstitutionRightCaptionPostDTO, CreditInstitutionRightCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
