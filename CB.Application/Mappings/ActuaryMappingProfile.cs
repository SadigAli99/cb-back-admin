

using CB.Application.DTOs.Actuary;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ActuaryMappingProfile : MappingProfile
    {
        public ActuaryMappingProfile() : base()
        {
            CreateMap<Actuary, ActuaryGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<Actuary, ActuaryGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<ActuaryCreateDTO, Actuary>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<ActuaryEditDTO, Actuary>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
