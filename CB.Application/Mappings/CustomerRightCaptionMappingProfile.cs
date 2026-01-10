
using CB.Application.DTOs.CustomerRightCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CustomerRightCaptionMappingProfile : MappingProfile
    {
        public CustomerRightCaptionMappingProfile()
        {
            CreateMap<CustomerRightCaption, CustomerRightCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<CustomerRightCaptionPostDTO, CustomerRightCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
