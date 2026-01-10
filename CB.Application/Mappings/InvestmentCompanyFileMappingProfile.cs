

using CB.Application.DTOs.InvestmentCompanyFile;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InvestmentCompanyFileMappingProfile : MappingProfile
    {
        public InvestmentCompanyFileMappingProfile() : base()
        {
            CreateMap<InvestmentCompanyFile, InvestmentCompanyFileGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<InvestmentCompanyFile, InvestmentCompanyFileGetDTO>>())
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


            CreateMap<InvestmentCompanyFileCreateDTO, InvestmentCompanyFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<InvestmentCompanyFileEditDTO, InvestmentCompanyFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
