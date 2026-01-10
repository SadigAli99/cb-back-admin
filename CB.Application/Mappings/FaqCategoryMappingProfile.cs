
using CB.Application.DTOs.FaqCategory;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class FaqCategoryMappingProfile : MappingProfile
    {
        public FaqCategoryMappingProfile() : base()
        {
            CreateMap<FaqCategory, FaqCategoryGetDTO>()
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    );

            CreateMap<FaqCategoryCreateDTO, FaqCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<FaqCategoryEditDTO, FaqCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
