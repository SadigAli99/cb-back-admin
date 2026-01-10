

using CB.Application.DTOs.InstantPaymentOrganization;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InstantPaymentOrganizationMappingProfile : MappingProfile
    {
        public InstantPaymentOrganizationMappingProfile() : base()
        {
            CreateMap<InstantPaymentOrganization, InstantPaymentOrganizationGetDTO>()
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


            CreateMap<InstantPaymentOrganizationCreateDTO, InstantPaymentOrganization>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<InstantPaymentOrganizationEditDTO, InstantPaymentOrganization>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
