using AutoMapper;
using CB.Application.DTOs.PaymentSystemStandartFAQ;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class PaymentSystemStandartFAQMappingProfile : Profile
    {
        public PaymentSystemStandartFAQMappingProfile()
        {
            CreateMap<PaymentSystemStandartFAQ, PaymentSystemStandartFAQGetDTO>()
                .ForMember(dest => dest.PaymentSystemStandartTitle, opt => opt.Ignore())
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

            CreateMap<PaymentSystemStandartFAQCreateDTO, PaymentSystemStandartFAQ>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<PaymentSystemStandartFAQEditDTO, PaymentSystemStandartFAQ>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
