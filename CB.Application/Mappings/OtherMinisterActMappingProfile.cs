

using CB.Application.DTOs.OtherMinisterAct;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class OtherMinisterActMappingProfile : MappingProfile
    {
        public OtherMinisterActMappingProfile() : base()
        {
            CreateMap<OtherMinisterAct, OtherMinisterActGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<OtherMinisterAct, OtherMinisterActGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<OtherMinisterActCreateDTO, OtherMinisterAct>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<OtherMinisterActEditDTO, OtherMinisterAct>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
