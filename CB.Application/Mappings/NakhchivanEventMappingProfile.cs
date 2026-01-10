

using CB.Application.DTOs.NakhchivanEvent;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class NakhchivanEventMappingProfile : MappingProfile
    {
        public NakhchivanEventMappingProfile() : base()
        {
            CreateMap<NakhchivanEvent, NakhchivanEventGetDTO>()
                .ForMember(dest => dest.Images, src => src.Ignore())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.Descriptions, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Description
                    ));
                });


            CreateMap<NakhchivanEventCreateDTO, NakhchivanEvent>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.Images, src => src.Ignore());

            CreateMap<NakhchivanEventEditDTO, NakhchivanEvent>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.Images, src => src.Ignore());

            CreateMap<NakhchivanEventImage, NakhchivanEventImageDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<NakhchivanEventImage, NakhchivanEventImageDTO>>());

        }
    }
}
