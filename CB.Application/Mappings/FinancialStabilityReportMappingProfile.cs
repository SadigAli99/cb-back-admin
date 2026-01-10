

using CB.Application.DTOs.FinancialStabilityReport;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class FinancialStabilityReportMappingProfile : MappingProfile
    {
        public FinancialStabilityReportMappingProfile() : base()
        {
            CreateMap<FinancialStabilityReport, FinancialStabilityReportGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<FinancialStabilityReport, FinancialStabilityReportGetDTO>>())
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


            CreateMap<FinancialStabilityReportCreateDTO, FinancialStabilityReport>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<FinancialStabilityReportEditDTO, FinancialStabilityReport>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
