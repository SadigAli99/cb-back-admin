
using CB.Application.DTOs.FraudStatisticCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class FraudStatisticCaptionMappingProfile : MappingProfile
    {
        public FraudStatisticCaptionMappingProfile()
        {
            CreateMap<FraudStatisticCaption, FraudStatisticCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<FraudStatisticCaptionPostDTO, FraudStatisticCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
