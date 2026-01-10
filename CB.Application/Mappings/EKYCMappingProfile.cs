
using CB.Application.DTOs.EKYC;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class EKYCMappingProfile : MappingProfile
    {
        public EKYCMappingProfile()
        {
            CreateMap<EKYC, EKYCGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<EKYCPostDTO, EKYC>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
