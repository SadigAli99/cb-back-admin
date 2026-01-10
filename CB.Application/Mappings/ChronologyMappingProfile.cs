

using CB.Application.DTOs.Chronology;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ChronologyMappingProfile : MappingProfile
    {
        public ChronologyMappingProfile() : base()
        {
            CreateMap<Chronology, ChronologyGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Description
                    ));
                });


            CreateMap<ChronologyCreateDTO, Chronology>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<ChronologyEditDTO, Chronology>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
