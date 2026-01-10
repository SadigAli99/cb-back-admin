

using CB.Application.DTOs.InsurerMinisterAct;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InsurerMinisterActMappingProfile : MappingProfile
    {
        public InsurerMinisterActMappingProfile() : base()
        {
            CreateMap<InsurerMinisterAct, InsurerMinisterActGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<InsurerMinisterAct, InsurerMinisterActGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<InsurerMinisterActCreateDTO, InsurerMinisterAct>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<InsurerMinisterActEditDTO, InsurerMinisterAct>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
