
using CB.Application.DTOs.InsuranceStatisticSubCategory;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InsuranceStatisticSubCategoryMappingProfile : MappingProfile
    {
        public InsuranceStatisticSubCategoryMappingProfile() : base()
        {
            CreateMap<InsuranceStatisticSubCategory, InsuranceStatisticSubCategoryGetDTO>()
                    .ForMember(dest => dest.InsuranceStatisticCategory, src => src.Ignore())
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    );

            CreateMap<InsuranceStatisticSubCategoryCreateDTO, InsuranceStatisticSubCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<InsuranceStatisticSubCategoryEditDTO, InsuranceStatisticSubCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
