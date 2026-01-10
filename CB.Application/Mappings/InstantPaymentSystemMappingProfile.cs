
using CB.Application.DTOs.InstantPaymentSystem;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InstantPaymentSystemMappingProfile : MappingProfile
    {
        public InstantPaymentSystemMappingProfile()
        {
            CreateMap<InstantPaymentSystem, InstantPaymentSystemGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<InstantPaymentSystemPostDTO, InstantPaymentSystem>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
