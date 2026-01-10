
using CB.Application.DTOs.IssuerType;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class IssuerTypeMappingProfile : MappingProfile
    {
        public IssuerTypeMappingProfile() : base()
        {
            CreateMap<IssuerType, IssuerTypeGetDTO>()
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    );

            CreateMap<IssuerTypeCreateDTO, IssuerType>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<IssuerTypeEditDTO, IssuerType>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
