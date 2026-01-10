

using CB.Application.DTOs.ExecutionStatus;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ExecutionStatusMappingProfile : MappingProfile
    {
        public ExecutionStatusMappingProfile() : base()
        {
            CreateMap<ExecutionStatus, ExecutionStatusGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<ExecutionStatus, ExecutionStatusGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<ExecutionStatusCreateDTO, ExecutionStatus>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<ExecutionStatusEditDTO, ExecutionStatus>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
