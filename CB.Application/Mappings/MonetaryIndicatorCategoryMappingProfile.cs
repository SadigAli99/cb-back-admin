
using CB.Application.DTOs.MonetaryIndicatorCategory;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MonetaryIndicatorCategoryMappingProfile : MappingProfile
    {
        public MonetaryIndicatorCategoryMappingProfile() : base()
        {
            CreateMap<MonetaryIndicatorCategory, MonetaryIndicatorCategoryGetDTO>()
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

            CreateMap<MonetaryIndicatorCategoryCreateDTO, MonetaryIndicatorCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<MonetaryIndicatorCategoryEditDTO, MonetaryIndicatorCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
