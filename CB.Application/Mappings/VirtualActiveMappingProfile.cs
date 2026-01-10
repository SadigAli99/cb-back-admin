
using CB.Application.DTOs.VirtualActive;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class VirtualActiveMappingProfile : MappingProfile
    {
        public VirtualActiveMappingProfile()
        {
            CreateMap<VirtualActive, VirtualActiveGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<VirtualActivePostDTO, VirtualActive>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
