
using CB.Application.DTOs.PaymentInstitutionCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class PaymentInstitutionCaptionMappingProfile : MappingProfile
    {
        public PaymentInstitutionCaptionMappingProfile()
        {
            CreateMap<PaymentInstitutionCaption, PaymentInstitutionCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<PaymentInstitutionCaptionPostDTO, PaymentInstitutionCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
