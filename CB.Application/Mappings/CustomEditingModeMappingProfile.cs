
using CB.Application.DTOs.CustomEditingMode;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CustomEditingModeMappingProfile : MappingProfile
    {
        public CustomEditingModeMappingProfile()
        {
            CreateMap<CustomEditingMode, CustomEditingModeGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<CustomEditingModePostDTO, CustomEditingMode>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
