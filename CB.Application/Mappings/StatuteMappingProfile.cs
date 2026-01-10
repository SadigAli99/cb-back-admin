

using CB.Application.DTOs.Statute;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class StatuteMappingProfile : MappingProfile
    {
        public StatuteMappingProfile() : base()
        {
            CreateMap<Statute, StatuteGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<Statute, StatuteGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.SubTitles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.SubTitle
                    ));
                });


            CreateMap<StatuteCreateDTO, Statute>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<StatuteEditDTO, Statute>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
