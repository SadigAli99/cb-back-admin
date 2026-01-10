

using CB.Application.DTOs.AnnualReport;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class AnnualReportMappingProfile : MappingProfile
    {
        public AnnualReportMappingProfile() : base()
        {
            CreateMap<AnnualReport, AnnualReportGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<AnnualReport, AnnualReportGetDTO>>())
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


            CreateMap<AnnualReportCreateDTO, AnnualReport>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<AnnualReportEditDTO, AnnualReport>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
