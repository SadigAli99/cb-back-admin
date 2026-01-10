using AutoMapper;
using CB.Application.DTOs.PaymentSystemControlService;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class PaymentSystemControlServiceMappingProfile : Profile
    {
        public PaymentSystemControlServiceMappingProfile()
        {
            CreateMap<PaymentSystemControlService, PaymentSystemControlServiceGetDTO>()
                .ForMember(dest => dest.PaymentSystemControlTitle, opt => opt.Ignore())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code, t => t.Title ?? string.Empty
                    ));
                })
                .ForMember(dest => dest.Descriptions, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code, t => t.Description ?? string.Empty
                    ));
                });

            CreateMap<PaymentSystemControlServiceCreateDTO, PaymentSystemControlService>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<PaymentSystemControlServiceEditDTO, PaymentSystemControlService>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
