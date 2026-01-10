
using CB.Application.DTOs.NakhchivanContact;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class NakhchivanContactMappingProfile : MappingProfile
    {
        public NakhchivanContactMappingProfile()
        {
            CreateMap<NakhchivanContact, NakhchivanContactGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<NakhchivanContactPostDTO, NakhchivanContact>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
