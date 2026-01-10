
using CB.Application.DTOs.MethodologyExplain;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MethodologyExplainMappingProfile : MappingProfile
    {
        public MethodologyExplainMappingProfile()
        {
            CreateMap<MethodologyExplain, MethodologyExplainGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<MethodologyExplainPostDTO, MethodologyExplain>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
