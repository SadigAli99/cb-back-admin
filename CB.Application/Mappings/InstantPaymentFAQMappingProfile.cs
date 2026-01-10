

using CB.Application.DTOs.InstantPaymentFAQ;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InstantPaymentFAQMappingProfile : MappingProfile
    {
        public InstantPaymentFAQMappingProfile() : base()
        {
            CreateMap<InstantPaymentFAQ, InstantPaymentFAQGetDTO>()
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


            CreateMap<InstantPaymentFAQCreateDTO, InstantPaymentFAQ>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<InstantPaymentFAQEditDTO, InstantPaymentFAQ>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
