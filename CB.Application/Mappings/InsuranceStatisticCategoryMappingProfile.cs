
using CB.Application.DTOs.InsuranceStatisticCategory;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InsuranceStatisticCategoryMappingProfile : MappingProfile
    {
        public InsuranceStatisticCategoryMappingProfile() : base()
        {
            CreateMap<InsuranceStatisticCategory, InsuranceStatisticCategoryGetDTO>()
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    );

            CreateMap<InsuranceStatisticCategoryCreateDTO, InsuranceStatisticCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<InsuranceStatisticCategoryEditDTO, InsuranceStatisticCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
