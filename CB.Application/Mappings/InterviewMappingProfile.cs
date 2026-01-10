

using CB.Application.DTOs.Interview;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InterviewMappingProfile : MappingProfile
    {
        public InterviewMappingProfile() : base()
        {
            CreateMap<Interview, InterviewGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<Interview, InterviewGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<InterviewCreateDTO, Interview>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<InterviewEditDTO, Interview>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
