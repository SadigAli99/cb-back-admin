

using CB.Application.DTOs.PaymentInstitution;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class PaymentInstitutionMappingProfile : MappingProfile
    {
        public PaymentInstitutionMappingProfile() : base()
        {
            CreateMap<PaymentInstitution, PaymentInstitutionGetDTO>()
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.Descriptions, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Description
                    ));
                });


            CreateMap<PaymentInstitutionCreateDTO, PaymentInstitution>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<PaymentInstitutionEditDTO, PaymentInstitution>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
