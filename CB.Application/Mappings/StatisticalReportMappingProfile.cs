

using CB.Application.DTOs.StatisticalReport;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class StatisticalReportMappingProfile : MappingProfile
    {
        public StatisticalReportMappingProfile() : base()
        {
            CreateMap<StatisticalReport, StatisticalReportGetDTO>()
                .ForMember(dest => dest.StatisticalReportCategory, src => src.Ignore())
                .ForMember(dest => dest.StatisticalReportSubCategory, src => src.Ignore())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.Periods, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Period
                    ));
                });


            CreateMap<StatisticalReportCreateDTO, StatisticalReport>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<StatisticalReportEditDTO, StatisticalReport>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
