

using CB.Application.DTOs.PaymentSystemStandart;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class PaymentSystemStandartMappingProfile : MappingProfile
    {
        public PaymentSystemStandartMappingProfile() : base()
        {
            CreateMap<PaymentSystemStandart, PaymentSystemStandartGetDTO>()
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


            CreateMap<PaymentSystemStandartCreateDTO, PaymentSystemStandart>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<PaymentSystemStandartEditDTO, PaymentSystemStandart>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
