
using CB.Application.DTOs.ClearingSettlementSystem;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ClearingSettlementSystemMappingProfile : MappingProfile
    {
        public ClearingSettlementSystemMappingProfile() : base()
        {
            CreateMap<ClearingSettlementSystem, ClearingSettlementSystemGetDTO>()
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    );

            CreateMap<ClearingSettlementSystemCreateDTO, ClearingSettlementSystem>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<ClearingSettlementSystemEditDTO, ClearingSettlementSystem>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
