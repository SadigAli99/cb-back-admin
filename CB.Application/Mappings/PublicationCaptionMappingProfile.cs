
using CB.Application.DTOs.PublicationCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class PublicationCaptionMappingProfile : MappingProfile
    {
        public PublicationCaptionMappingProfile()
        {
            CreateMap<PublicationCaption, PublicationCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<PublicationCaptionPostDTO, PublicationCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
