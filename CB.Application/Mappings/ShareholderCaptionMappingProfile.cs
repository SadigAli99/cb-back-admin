
using CB.Application.DTOs.ShareholderCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ShareholderCaptionMappingProfile : MappingProfile
    {
        public ShareholderCaptionMappingProfile()
        {
            CreateMap<ShareholderCaption, ShareholderCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<ShareholderCaptionPostDTO, ShareholderCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
