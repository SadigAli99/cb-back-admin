
using CB.Application.DTOs.OperatorBankCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class OperatorBankCaptionMappingProfile : MappingProfile
    {
        public OperatorBankCaptionMappingProfile()
        {
            CreateMap<OperatorBankCaption, OperatorBankCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<OperatorBankCaptionPostDTO, OperatorBankCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
