
using CB.Application.DTOs.PaymentSystemOperationCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class PaymentSystemOperationCaptionMappingProfile : MappingProfile
    {
        public PaymentSystemOperationCaptionMappingProfile()
        {
            CreateMap<PaymentSystemOperationCaption, PaymentSystemOperationCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<PaymentSystemOperationCaptionPostDTO, PaymentSystemOperationCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
