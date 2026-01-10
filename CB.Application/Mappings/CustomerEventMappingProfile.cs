

using CB.Application.DTOs.CustomerEvent;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CustomerEventMappingProfile : MappingProfile
    {
        public CustomerEventMappingProfile() : base()
        {
            CreateMap<CustomerEvent, CustomerEventGetDTO>()
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


            CreateMap<CustomerEventCreateDTO, CustomerEvent>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.Images, src => src.Ignore());

            CreateMap<CustomerEventEditDTO, CustomerEvent>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.Images, src => src.Ignore());

            CreateMap<CustomerEventImage, CustomerEventImageDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<CustomerEventImage, CustomerEventImageDTO>>());

        }
    }
}
