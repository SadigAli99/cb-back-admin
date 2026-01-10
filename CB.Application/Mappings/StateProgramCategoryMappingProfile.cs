
using CB.Application.DTOs.StateProgramCategory;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class StateProgramCategoryMappingProfile : MappingProfile
    {
        public StateProgramCategoryMappingProfile() : base()
        {
            CreateMap<StateProgramCategory, StateProgramCategoryGetDTO>()
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    );

            CreateMap<StateProgramCategoryCreateDTO, StateProgramCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<StateProgramCategoryEditDTO, StateProgramCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
