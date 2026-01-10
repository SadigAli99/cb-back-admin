
using CB.Application.DTOs.ActuaryCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ActuaryCaptionMappingProfile : MappingProfile
    {
        public ActuaryCaptionMappingProfile()
        {
            CreateMap<ActuaryCaption, ActuaryCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<ActuaryCaptionPostDTO, ActuaryCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
