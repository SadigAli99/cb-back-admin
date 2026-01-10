
using CB.Application.DTOs.NationalBankNoteCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class NationalBankNoteCaptionMappingProfile : MappingProfile
    {
        public NationalBankNoteCaptionMappingProfile()
        {
            CreateMap<NationalBankNoteCaption, NationalBankNoteCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<NationalBankNoteCaptionPostDTO, NationalBankNoteCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
