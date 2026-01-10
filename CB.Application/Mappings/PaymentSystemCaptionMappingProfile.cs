
using CB.Application.DTOs.PaymentSystemCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class PaymentSystemCaptionMappingProfile : MappingProfile
    {
        public PaymentSystemCaptionMappingProfile()
        {
            CreateMap<PaymentSystemCaption, PaymentSystemCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<PaymentSystemCaptionPostDTO, PaymentSystemCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
