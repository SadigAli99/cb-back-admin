
using CB.Application.DTOs.BankSectorCategory;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class BankSectorCategoryMappingProfile : MappingProfile
    {
        public BankSectorCategoryMappingProfile() : base()
        {
            CreateMap<BankSectorCategory, BankSectorCategoryGetDTO>()
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

            CreateMap<BankSectorCategoryCreateDTO, BankSectorCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<BankSectorCategoryEditDTO, BankSectorCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
