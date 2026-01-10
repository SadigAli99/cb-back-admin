
using CB.Application.DTOs.VirtualEducationCaption;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class VirtualEducationCaptionMappingProfile : MappingProfile
    {
        public VirtualEducationCaptionMappingProfile()
        {
            CreateMap<VirtualEducationCaption, VirtualEducationCaptionGetDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<VirtualEducationCaption, VirtualEducationCaptionGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Title
                        )
                    )
                );

            CreateMap<VirtualEducationCaptionPostDTO, VirtualEducationCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
