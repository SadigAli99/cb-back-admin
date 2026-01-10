

using CB.Application.DTOs.PaymentSystemControl;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class PaymentSystemControlMappingProfile : MappingProfile
    {
        public PaymentSystemControlMappingProfile() : base()
        {
            CreateMap<PaymentSystemControl, PaymentSystemControlGetDTO>()
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.Slugs, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Slug
                    ));
                })
                .ForMember(dest => dest.Descriptions, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Description
                    ));
                });


            CreateMap<PaymentSystemControlCreateDTO, PaymentSystemControl>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<PaymentSystemControlEditDTO, PaymentSystemControl>()
                .ForMember(dest => dest.Translations, src => src.Ignore());
        }
    }
}
