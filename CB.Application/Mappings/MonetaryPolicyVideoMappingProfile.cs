
using CB.Application.DTOs.MonetaryPolicyVideo;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MonetaryPolicyVideoMappingProfile : MappingProfile
    {
        public MonetaryPolicyVideoMappingProfile()
        {
            CreateMap<MonetaryPolicyVideo, MonetaryPolicyVideoGetDTO>();

            CreateMap<MonetaryPolicyVideoPostDTO, MonetaryPolicyVideo>()
                .ReverseMap();
        }
    }
}
