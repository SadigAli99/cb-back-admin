
using CB.Application.DTOs.RevisionPolicy;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class RevisionPolicyMappingProfile : MappingProfile
    {
        public RevisionPolicyMappingProfile()
        {
            CreateMap<RevisionPolicy, RevisionPolicyGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<RevisionPolicyPostDTO, RevisionPolicy>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
