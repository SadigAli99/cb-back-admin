
using CB.Application.DTOs.NominationCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class NominationCaptionMappingProfile : MappingProfile
    {
        public NominationCaptionMappingProfile()
        {
            CreateMap<NominationCaption, NominationCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<NominationCaptionPostDTO, NominationCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
