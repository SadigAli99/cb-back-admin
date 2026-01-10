using AutoMapper;
using CB.Application.DTOs.EventImage;
using CB.Core.Entities;
using CB.Application.Mappings.Resolvers;

namespace CB.Application.Mappings
{
    public class EventImageMappingProfile : Profile
    {
        public EventImageMappingProfile()
        {
            CreateMap<EventMedia, EventImageGetDTO>()
                .ForMember(dest => dest.Image,
                    opt => opt.MapFrom<GenericResolver<EventMedia, EventImageGetDTO>>());
            CreateMap<EventImageCreateDTO, EventMedia>()
                .ForMember(dest => dest.Url, opt => opt.Ignore());
        }
    }
}
