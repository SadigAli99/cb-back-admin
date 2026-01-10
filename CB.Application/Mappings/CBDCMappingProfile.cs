
using CB.Application.DTOs.CBDC;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CBDCMappingProfile : MappingProfile
    {
        public CBDCMappingProfile()
        {
            CreateMap<CBDC, CBDCGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<CBDCPostDTO, CBDC>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
