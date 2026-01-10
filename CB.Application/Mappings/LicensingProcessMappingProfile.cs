

using CB.Application.DTOs.LicensingProcess;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class LicensingProcessMappingProfile : MappingProfile
    {
        public LicensingProcessMappingProfile() : base()
        {
            CreateMap<LicensingProcess, LicensingProcessGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<LicensingProcess, LicensingProcessGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<LicensingProcessCreateDTO, LicensingProcess>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<LicensingProcessEditDTO, LicensingProcess>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
