
using CB.Application.DTOs.InsuranceBrokerCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InsuranceBrokerCaptionMappingProfile : MappingProfile
    {
        public InsuranceBrokerCaptionMappingProfile()
        {
            CreateMap<InsuranceBrokerCaption, InsuranceBrokerCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<InsuranceBrokerCaptionPostDTO, InsuranceBrokerCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
