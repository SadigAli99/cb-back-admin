
using CB.Application.DTOs.StructureCaption;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class StructureCaptionMappingProfile : MappingProfile
    {
        public StructureCaptionMappingProfile()
        {
            CreateMap<StructureCaption, StructureCaptionGetDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<StructureCaption, StructureCaptionGetDTO>>())
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<StructureCaptionPostDTO, StructureCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
