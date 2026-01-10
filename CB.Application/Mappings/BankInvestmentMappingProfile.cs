

using CB.Application.DTOs.BankInvestment;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class BankInvestmentMappingProfile : MappingProfile
    {
        public BankInvestmentMappingProfile() : base()
        {
            CreateMap<BankInvestment, BankInvestmentGetDTO>()
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


            CreateMap<BankInvestmentCreateDTO, BankInvestment>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<BankInvestmentEditDTO, BankInvestment>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
