
using CB.Application.DTOs.CBAR105Caption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CBAR105CaptionMappingProfile : MappingProfile
    {
        public CBAR105CaptionMappingProfile()
        {
            CreateMap<CBAR105Caption, CBAR105CaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<CBAR105CaptionPostDTO, CBAR105Caption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
