

using CB.Application.DTOs.AttestationFile;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class AttestationFileMappingProfile : MappingProfile
    {
        public AttestationFileMappingProfile() : base()
        {
            CreateMap<AttestationFile, AttestationFileGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<AttestationFile, AttestationFileGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<AttestationFileCreateDTO, AttestationFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<AttestationFileEditDTO, AttestationFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
