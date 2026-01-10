
using CB.Application.DTOs.EconometricModel;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class EconometricModelMappingProfile : MappingProfile
    {
        public EconometricModelMappingProfile()
        {
            CreateMap<EconometricModel, EconometricModelGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<EconometricModelPostDTO, EconometricModel>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
