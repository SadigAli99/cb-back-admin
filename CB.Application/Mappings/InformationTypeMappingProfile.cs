
using CB.Application.DTOs.InformationType;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InformationTypeMappingProfile : MappingProfile
    {
        public InformationTypeMappingProfile() : base()
        {
            CreateMap<InformationType, InformationTypeGetDTO>()
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    );

            CreateMap<InformationTypeCreateDTO, InformationType>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<InformationTypeEditDTO, InformationType>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
