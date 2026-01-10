
using CB.Application.DTOs.BankNoteCategory;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class BankNoteCategoryMappingProfile : MappingProfile
    {
        public BankNoteCategoryMappingProfile() : base()
        {
            CreateMap<BankNoteCategory, BankNoteCategoryGetDTO>()
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    )
                    .ForMember(dest => dest.ShortTitles, opt =>
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

            CreateMap<BankNoteCategoryCreateDTO, BankNoteCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<BankNoteCategoryEditDTO, BankNoteCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
