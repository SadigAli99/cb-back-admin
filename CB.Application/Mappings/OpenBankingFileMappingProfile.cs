

using CB.Application.DTOs.OpenBankingFile;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class OpenBankingFileMappingProfile : MappingProfile
    {
        public OpenBankingFileMappingProfile() : base()
        {
            CreateMap<OpenBankingFile, OpenBankingFileGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<OpenBankingFile, OpenBankingFileGetDTO>>())
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


            CreateMap<OpenBankingFileCreateDTO, OpenBankingFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<OpenBankingFileEditDTO, OpenBankingFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
