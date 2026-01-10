
using CB.Application.DTOs.OutOfBankNoteMoneySignHistoryFeature;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class OutOfBankNoteMoneySignHistoryFeatureMappingProfile : MappingProfile
    {
        public OutOfBankNoteMoneySignHistoryFeatureMappingProfile()
        {
            CreateMap<MoneySignHistoryFeature, OutOfBankNoteMoneySignHistoryFeatureGetDTO>()
                .ForMember(dest => dest.MoneySignHistoryTitle, src => src.Ignore())
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<OutOfBankNoteMoneySignHistoryFeaturePostDTO, MoneySignHistoryFeature>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
