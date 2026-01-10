
using CB.Application.DTOs.CapitalMarketCategory;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CapitalMarketCategoryMappingProfile : MappingProfile
    {
        public CapitalMarketCategoryMappingProfile() : base()
        {
            CreateMap<CapitalMarketCategory, CapitalMarketCategoryGetDTO>()
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    );

            CreateMap<CapitalMarketCategoryCreateDTO, CapitalMarketCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<CapitalMarketCategoryEditDTO, CapitalMarketCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
