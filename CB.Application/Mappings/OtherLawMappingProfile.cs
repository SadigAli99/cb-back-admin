

using CB.Application.DTOs.OtherLaw;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class OtherLawMappingProfile : MappingProfile
    {
        public OtherLawMappingProfile() : base()
        {
            CreateMap<OtherLaw, OtherLawGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<OtherLaw, OtherLawGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<OtherLawCreateDTO, OtherLaw>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<OtherLawEditDTO, OtherLaw>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
