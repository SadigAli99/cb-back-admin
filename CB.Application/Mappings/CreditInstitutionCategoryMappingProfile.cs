
using CB.Application.DTOs.CreditInstitutionCategory;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CreditInstitutionCategoryMappingProfile : MappingProfile
    {
        public CreditInstitutionCategoryMappingProfile() : base()
        {
            CreateMap<CreditInstitutionCategory, CreditInstitutionCategoryGetDTO>()
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    );

            CreateMap<CreditInstitutionCategoryCreateDTO, CreditInstitutionCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<CreditInstitutionCategoryEditDTO, CreditInstitutionCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
