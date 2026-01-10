
using CB.Application.DTOs.FinancialInstitution;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class FinancialInstitutionMappingProfile : MappingProfile
    {
        public FinancialInstitutionMappingProfile()
        {
            CreateMap<FinancialInstitution, FinancialInstitutionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<FinancialInstitutionPostDTO, FinancialInstitution>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
