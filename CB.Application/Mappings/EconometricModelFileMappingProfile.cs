

using CB.Application.DTOs.EconometricModelFile;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class EconometricModelFileMappingProfile : MappingProfile
    {
        public EconometricModelFileMappingProfile() : base()
        {
            CreateMap<EconometricModelFile, EconometricModelFileGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<EconometricModelFile, EconometricModelFileGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.CoverTitles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.CoverTitle
                    ));
                });


            CreateMap<EconometricModelFileCreateDTO, EconometricModelFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<EconometricModelFileEditDTO, EconometricModelFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
