

using CB.Application.DTOs.ReceptionCitizenVideo;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ReceptionCitizenVideoMappingProfile : MappingProfile
    {
        public ReceptionCitizenVideoMappingProfile() : base()
        {
            CreateMap<ReceptionCitizenVideo, ReceptionCitizenVideoGetDTO>()
                .ForMember(dest => dest.ReceptionCitizenCategoryTitle, src => src.Ignore());


            CreateMap<ReceptionCitizenVideoCreateDTO, ReceptionCitizenVideo>();

            CreateMap<ReceptionCitizenVideoEditDTO, ReceptionCitizenVideo>();

        }
    }
}
