using AutoMapper;
using CB.Application.DTOs.Event;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class EventMappingProfile : Profile
    {
        public EventMappingProfile()
        {
            CreateMap<Event, EventGetDTO>()
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code, t => t.Title ?? string.Empty
                    ));
                })
                .ForMember(dest => dest.Slugs, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code, t => t.Slug ?? string.Empty
                    ));
                })
                .ForMember(dest => dest.Descriptions, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code, t => t.Description ?? string.Empty
                    ));
                });

            CreateMap<EventCreateDTO, Event>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<EventEditDTO, Event>()
                .ForMember(dest => dest.Translations, src => src.Ignore());
        }
    }
}
