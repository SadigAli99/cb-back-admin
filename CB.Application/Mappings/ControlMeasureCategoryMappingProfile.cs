
using CB.Application.DTOs.ControlMeasureCategory;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ControlMeasureCategoryMappingProfile : MappingProfile
    {
        public ControlMeasureCategoryMappingProfile() : base()
        {
            CreateMap<ControlMeasureCategory, ControlMeasureCategoryGetDTO>()
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    );

            CreateMap<ControlMeasureCategoryCreateDTO, ControlMeasureCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<ControlMeasureCategoryEditDTO, ControlMeasureCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
