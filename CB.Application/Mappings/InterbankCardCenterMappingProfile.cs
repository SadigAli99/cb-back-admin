
using CB.Application.DTOs.InterbankCardCenter;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InterbankCardCenterMappingProfile : MappingProfile
    {
        public InterbankCardCenterMappingProfile()
        {
            CreateMap<InterbankCardCenter, InterbankCardCenterGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<InterbankCardCenterPostDTO, InterbankCardCenter>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
