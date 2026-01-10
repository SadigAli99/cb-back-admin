
using CB.Application.DTOs.OutOfCirculationCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class OutOfCirculationCaptionMappingProfile : MappingProfile
    {
        public OutOfCirculationCaptionMappingProfile()
        {
            CreateMap<OutOfCirculationCaption, OutOfCirculationCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<OutOfCirculationCaptionPostDTO, OutOfCirculationCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
