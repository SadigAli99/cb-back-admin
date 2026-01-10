
using CB.Application.DTOs.MoneySignProtectionCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MoneySignProtectionCaptionMappingProfile : MappingProfile
    {
        public MoneySignProtectionCaptionMappingProfile()
        {
            CreateMap<MoneySignProtectionCaption, MoneySignProtectionCaptionGetDTO>()
                .ForMember(dest => dest.MoneySignHistoryTitle, src => src.Ignore())
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<MoneySignProtectionCaptionPostDTO, MoneySignProtectionCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
