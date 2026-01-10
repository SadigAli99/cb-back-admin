
using CB.Application.DTOs.NominationCategory;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class NominationCategoryMappingProfile : MappingProfile
    {
        public NominationCategoryMappingProfile() : base()
        {
            CreateMap<NominationCategory, NominationCategoryGetDTO>()
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    );

            CreateMap<NominationCategoryCreateDTO, NominationCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<NominationCategoryEditDTO, NominationCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
