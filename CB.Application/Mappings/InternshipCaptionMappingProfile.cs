
using CB.Application.DTOs.InternshipCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InternshipCaptionMappingProfile : MappingProfile
    {
        public InternshipCaptionMappingProfile()
        {
            CreateMap<InternshipCaption, InternshipCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<InternshipCaptionPostDTO, InternshipCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
