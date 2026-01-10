
using CB.Application.DTOs.CentralBankCooperationCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CentralBankCooperationCaptionMappingProfile : MappingProfile
    {
        public CentralBankCooperationCaptionMappingProfile()
        {
            CreateMap<CentralBankCooperationCaption, CentralBankCooperationCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<CentralBankCooperationCaptionPostDTO, CentralBankCooperationCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
