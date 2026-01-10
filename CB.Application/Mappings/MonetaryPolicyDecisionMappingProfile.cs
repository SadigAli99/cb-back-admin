

using CB.Application.DTOs.MonetaryPolicyDecision;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MonetaryPolicyDecisionMappingProfile : MappingProfile
    {
        public MonetaryPolicyDecisionMappingProfile() : base()
        {
            CreateMap<MonetaryPolicyDecision, MonetaryPolicyDecisionGetDTO>()
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


            CreateMap<MonetaryPolicyDecisionCreateDTO, MonetaryPolicyDecision>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<MonetaryPolicyDecisionEditDTO, MonetaryPolicyDecision>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
