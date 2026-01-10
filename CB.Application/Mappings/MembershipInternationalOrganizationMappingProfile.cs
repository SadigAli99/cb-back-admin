

using CB.Application.DTOs.MembershipInternationalOrganization;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MembershipInternationalOrganizationMappingProfile : MappingProfile
    {
        public MembershipInternationalOrganizationMappingProfile() : base()
        {
            CreateMap<MembershipInternationalOrganization, MembershipInternationalOrganizationGetDTO>()
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<MembershipInternationalOrganizationCreateDTO, MembershipInternationalOrganization>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<MembershipInternationalOrganizationEditDTO, MembershipInternationalOrganization>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
