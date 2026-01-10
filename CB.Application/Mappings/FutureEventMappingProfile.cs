

using CB.Application.DTOs.FutureEvent;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class FutureEventMappingProfile : MappingProfile
    {
        public FutureEventMappingProfile() : base()
        {
            CreateMap<FutureEvent, FutureEventGetDTO>()
                .ForMember(dest => dest.Date, src => src.MapFrom(x => x.Date.ToString("dd.MM.yyyy")))
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.Locations, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Location
                    ));
                })
                .ForMember(dest => dest.Formats, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Format
                    ));
                });


            CreateMap<FutureEventCreateDTO, FutureEvent>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<FutureEventEditDTO, FutureEvent>()
                .ForMember(dest => dest.Translations, src => src.Ignore());
        }
    }
}
