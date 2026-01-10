

using CB.Application.DTOs.InsurerLaw;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InsurerLawMappingProfile : MappingProfile
    {
        public InsurerLawMappingProfile() : base()
        {
            CreateMap<InsurerLaw, InsurerLawGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<InsurerLaw, InsurerLawGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<InsurerLawCreateDTO, InsurerLaw>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<InsurerLawEditDTO, InsurerLaw>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
