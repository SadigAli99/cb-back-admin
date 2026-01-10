
using CB.Application.DTOs.PercentCorridorCategory;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class PercentCorridorCategoryMappingProfile : MappingProfile
    {
        public PercentCorridorCategoryMappingProfile() : base()
        {
            CreateMap<PercentCorridorCategory, PercentCorridorCategoryGetDTO>()
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    )
                    .ForMember(dest => dest.Slugs, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Slug
                            )
                        )
                    )
                    .ForMember(dest => dest.Notes, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Note
                            )
                        )
                    );

            CreateMap<PercentCorridorCategoryCreateDTO, PercentCorridorCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<PercentCorridorCategoryEditDTO, PercentCorridorCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
