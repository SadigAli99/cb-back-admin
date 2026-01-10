
using CB.Application.DTOs.ComplaintIndexCategory;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ComplaintIndexCategoryMappingProfile : MappingProfile
    {
        public ComplaintIndexCategoryMappingProfile() : base()
        {
            CreateMap<ComplaintIndexCategory, ComplaintIndexCategoryGetDTO>()
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    );

            CreateMap<ComplaintIndexCategoryCreateDTO, ComplaintIndexCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<ComplaintIndexCategoryEditDTO, ComplaintIndexCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
