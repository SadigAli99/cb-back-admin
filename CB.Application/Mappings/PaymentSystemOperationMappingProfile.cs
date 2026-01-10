

using CB.Application.DTOs.PaymentSystemOperation;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class PaymentSystemOperationMappingProfile : MappingProfile
    {
        public PaymentSystemOperationMappingProfile() : base()
        {
            CreateMap<PaymentSystemOperation, PaymentSystemOperationGetDTO>()
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


            CreateMap<PaymentSystemOperationCreateDTO, PaymentSystemOperation>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<PaymentSystemOperationEditDTO, PaymentSystemOperation>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
