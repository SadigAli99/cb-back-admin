
using CB.Application.DTOs.InfographicDisclosureCategory;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InfographicDisclosureCategoryMappingProfile : MappingProfile
    {
        public InfographicDisclosureCategoryMappingProfile() : base()
        {
            CreateMap<InfographicDisclosureCategory, InfographicDisclosureCategoryGetDTO>()
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    );

            CreateMap<InfographicDisclosureCategoryCreateDTO, InfographicDisclosureCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<InfographicDisclosureCategoryEditDTO, InfographicDisclosureCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
