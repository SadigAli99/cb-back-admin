
using CB.Application.DTOs.PaymentAgentCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class PaymentAgentCaptionMappingProfile : MappingProfile
    {
        public PaymentAgentCaptionMappingProfile()
        {
            CreateMap<PaymentAgentCaption, PaymentAgentCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<PaymentAgentCaptionPostDTO, PaymentAgentCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
