
using CB.Application.DTOs.GreenTaxonomy;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class GreenTaxonomyMappingProfile : MappingProfile
    {
        public GreenTaxonomyMappingProfile()
        {
            CreateMap<GreenTaxonomy, GreenTaxonomyGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<GreenTaxonomyPostDTO, GreenTaxonomy>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
