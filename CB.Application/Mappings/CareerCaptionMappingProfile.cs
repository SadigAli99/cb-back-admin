
using CB.Application.DTOs.CareerCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CareerCaptionMappingProfile : MappingProfile
    {
        public CareerCaptionMappingProfile()
        {
            CreateMap<CareerCaption, CareerCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<CareerCaptionPostDTO, CareerCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
