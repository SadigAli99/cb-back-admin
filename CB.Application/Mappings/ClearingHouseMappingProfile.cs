
using CB.Application.DTOs.ClearingHouse;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ClearingHouseMappingProfile : MappingProfile
    {
        public ClearingHouseMappingProfile()
        {
            CreateMap<ClearingHouse, ClearingHouseGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<ClearingHousePostDTO, ClearingHouse>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
