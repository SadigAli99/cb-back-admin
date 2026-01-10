
using CB.Application.DTOs.RealTimeSettlementSystem;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class RealTimeSettlementSystemMappingProfile : MappingProfile
    {
        public RealTimeSettlementSystemMappingProfile() : base()
        {
            CreateMap<RealTimeSettlementSystem, RealTimeSettlementSystemGetDTO>()
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    );

            CreateMap<RealTimeSettlementSystemCreateDTO, RealTimeSettlementSystem>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<RealTimeSettlementSystemEditDTO, RealTimeSettlementSystem>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
