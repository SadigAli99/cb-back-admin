

using CB.Application.DTOs.OtherPresidentAct;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class OtherPresidentActMappingProfile : MappingProfile
    {
        public OtherPresidentActMappingProfile() : base()
        {
            CreateMap<OtherPresidentAct, OtherPresidentActGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<OtherPresidentAct, OtherPresidentActGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<OtherPresidentActCreateDTO, OtherPresidentAct>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<OtherPresidentActEditDTO, OtherPresidentAct>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
