
using CB.Application.DTOs.CustomerFeedbackCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CustomerFeedbackCaptionMappingProfile : MappingProfile
    {
        public CustomerFeedbackCaptionMappingProfile()
        {
            CreateMap<CustomerFeedbackCaption, CustomerFeedbackCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<CustomerFeedbackCaptionPostDTO, CustomerFeedbackCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
