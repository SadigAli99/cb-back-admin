
using CB.Application.DTOs.ReceptionCitizenCategory;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ReceptionCitizenCategoryMappingProfile : MappingProfile
    {
        public ReceptionCitizenCategoryMappingProfile() : base()
        {
            CreateMap<ReceptionCitizenCategory, ReceptionCitizenCategoryGetDTO>()
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    )
                    .ForMember(dest => dest.Descriptions, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Description
                            )
                        )
                    );

            CreateMap<ReceptionCitizenCategoryCreateDTO, ReceptionCitizenCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<ReceptionCitizenCategoryEditDTO, ReceptionCitizenCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
