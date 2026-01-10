using AutoMapper;
using CB.Application.DTOs.InternationalEvent;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InternationalEventMappingProfile : Profile
    {
        public InternationalEventMappingProfile()
        {
            CreateMap<InternationalEvent, InternationalEventGetDTO>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
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

            CreateMap<InternationalEventCreateDTO, InternationalEvent>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.Images, src => src.Ignore());

            CreateMap<InternationalEventEditDTO, InternationalEvent>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.Images, src => src.Ignore());

            CreateMap<InternationalEventImage, InternationalEventImageDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<InternationalEventImage, InternationalEventImageDTO>>());
        }
    }
}
