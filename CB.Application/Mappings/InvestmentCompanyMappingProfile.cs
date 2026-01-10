

using CB.Application.DTOs.InvestmentCompany;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InvestmentCompanyMappingProfile : MappingProfile
    {
        public InvestmentCompanyMappingProfile() : base()
        {
            CreateMap<InvestmentCompany, InvestmentCompanyGetDTO>()
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.Descriptions, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Description
                    ));
                });


            CreateMap<InvestmentCompanyCreateDTO, InvestmentCompany>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<InvestmentCompanyEditDTO, InvestmentCompany>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
