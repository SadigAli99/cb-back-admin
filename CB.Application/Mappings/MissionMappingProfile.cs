

using CB.Application.DTOs.Mission;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MissionMappingProfile : MappingProfile
    {
        public MissionMappingProfile() : base()
        {
            CreateMap<Mission, MissionGetDTO>()
                .ForMember(dest=>dest.Icon, src=>src.MapFrom<GenericResolver<Mission,MissionGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.Texts, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Text
                    ));
                });


            CreateMap<MissionCreateDTO, Mission>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<MissionEditDTO, Mission>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
