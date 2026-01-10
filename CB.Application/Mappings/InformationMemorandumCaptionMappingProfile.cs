
using CB.Application.DTOs.InformationMemorandumCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InformationMemorandumCaptionMappingProfile : MappingProfile
    {
        public InformationMemorandumCaptionMappingProfile()
        {
            CreateMap<InformationMemorandumCaption, InformationMemorandumCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<InformationMemorandumCaptionPostDTO, InformationMemorandumCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
