

using CB.Application.DTOs.StatisticalReportFile;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class StatisticalReportFileMappingProfile : MappingProfile
    {
        public StatisticalReportFileMappingProfile() : base()
        {
            CreateMap<StatisticalReportFile, StatisticalReportFileGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<StatisticalReportFile, StatisticalReportFileGetDTO>>())
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


            CreateMap<StatisticalReportFileCreateDTO, StatisticalReportFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<StatisticalReportFileEditDTO, StatisticalReportFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
