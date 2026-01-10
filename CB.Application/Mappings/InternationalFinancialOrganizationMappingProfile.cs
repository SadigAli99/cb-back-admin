
using CB.Application.DTOs.InternationalFinancialOrganization;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InternationalFinancialOrganizationMappingProfile : MappingProfile
    {
        public InternationalFinancialOrganizationMappingProfile()
        {
            CreateMap<InternationalFinancialOrganization, InternationalFinancialOrganizationGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<InternationalFinancialOrganizationPostDTO, InternationalFinancialOrganization>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
