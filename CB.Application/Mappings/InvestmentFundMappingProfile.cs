

using CB.Application.DTOs.InvestmentFund;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InvestmentFundMappingProfile : MappingProfile
    {
        public InvestmentFundMappingProfile() : base()
        {
            CreateMap<InvestmentFund, InvestmentFundGetDTO>()
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


            CreateMap<InvestmentFundCreateDTO, InvestmentFund>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<InvestmentFundEditDTO, InvestmentFund>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
