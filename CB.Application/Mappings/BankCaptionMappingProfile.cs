
using CB.Application.DTOs.BankCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class BankCaptionMappingProfile : MappingProfile
    {
        public BankCaptionMappingProfile()
        {
            CreateMap<BankCaption, BankCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<BankCaptionPostDTO, BankCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
