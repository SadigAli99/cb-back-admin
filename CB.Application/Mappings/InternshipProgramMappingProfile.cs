
using CB.Application.DTOs.InternshipProgram;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InternshipProgramMappingProfile : MappingProfile
    {
        public InternshipProgramMappingProfile()
        {
            CreateMap<InternshipProgram, InternshipProgramGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<InternshipProgramPostDTO, InternshipProgram>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
