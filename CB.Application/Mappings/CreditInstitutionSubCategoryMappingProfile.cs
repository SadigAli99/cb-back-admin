
using CB.Application.DTOs.CreditInstitutionSubCategory;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CreditInstitutionSubCategoryMappingProfile : MappingProfile
    {
        public CreditInstitutionSubCategoryMappingProfile() : base()
        {
            CreateMap<CreditInstitutionSubCategory, CreditInstitutionSubCategoryGetDTO>()
                    .ForMember(dest => dest.CreditInstitutionCategory, src => src.Ignore())
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    );

            CreateMap<CreditInstitutionSubCategoryCreateDTO, CreditInstitutionSubCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<CreditInstitutionSubCategoryEditDTO, CreditInstitutionSubCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
