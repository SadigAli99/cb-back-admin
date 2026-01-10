
using CB.Application.DTOs.NonBankCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class NonBankCaptionMappingProfile : MappingProfile
    {
        public NonBankCaptionMappingProfile()
        {
            CreateMap<NonBankCaption, NonBankCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<NonBankCaptionPostDTO, NonBankCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
