
using CB.Application.DTOs.DepositorySystem;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class DepositorySystemMappingProfile : MappingProfile
    {
        public DepositorySystemMappingProfile()
        {
            CreateMap<DepositorySystem, DepositorySystemGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<DepositorySystemPostDTO, DepositorySystem>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
