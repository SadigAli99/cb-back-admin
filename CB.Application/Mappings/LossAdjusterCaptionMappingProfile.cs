
using CB.Application.DTOs.LossAdjusterCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class LossAdjusterCaptionMappingProfile : MappingProfile
    {
        public LossAdjusterCaptionMappingProfile()
        {
            CreateMap<LossAdjusterCaption, LossAdjusterCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<LossAdjusterCaptionPostDTO, LossAdjusterCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
