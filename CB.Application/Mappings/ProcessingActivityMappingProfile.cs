
using CB.Application.DTOs.ProcessingActivity;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ProcessingActivityMappingProfile : MappingProfile
    {
        public ProcessingActivityMappingProfile()
        {
            CreateMap<ProcessingActivity, ProcessingActivityGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<ProcessingActivityPostDTO, ProcessingActivity>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
