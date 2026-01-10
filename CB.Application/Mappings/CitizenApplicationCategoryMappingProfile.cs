
using CB.Application.DTOs.CitizenApplicationCategory;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CitizenApplicationCategoryMappingProfile : MappingProfile
    {
        public CitizenApplicationCategoryMappingProfile() : base()
        {
            CreateMap<CitizenApplicationCategory, CitizenApplicationCategoryGetDTO>()
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    );

            CreateMap<CitizenApplicationCategoryCreateDTO, CitizenApplicationCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<CitizenApplicationCategoryEditDTO, CitizenApplicationCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
