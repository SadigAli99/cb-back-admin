using AutoMapper;
using CB.Application.DTOs.EventVideo;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class EventVideoMappingProfile : Profile
    {
        public EventVideoMappingProfile()
        {
            CreateMap<EventMedia, EventVideoGetDTO>()
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url));

            CreateMap<EventVideoCreateDTO, EventMedia>()
                .ForMember(dest => dest.MediaType, opt => opt.MapFrom(src => CB.Core.Enums.MediaType.VIDEO));

            CreateMap<EventVideoEditDTO, EventMedia>()
                .ForMember(dest => dest.MediaType, opt => opt.Ignore());
        }
    }
}
