

using CB.Application.DTOs.ReceptionCitizenFile;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ReceptionCitizenFileMappingProfile : MappingProfile
    {
        public ReceptionCitizenFileMappingProfile() : base()
        {
            CreateMap<ReceptionCitizenFile, ReceptionCitizenFileGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<ReceptionCitizenFile, ReceptionCitizenFileGetDTO>>())
                .ForMember(dest => dest.ReceptionCitizenTitle, src => src.Ignore())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<ReceptionCitizenFileCreateDTO, ReceptionCitizenFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<ReceptionCitizenFileEditDTO, ReceptionCitizenFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
