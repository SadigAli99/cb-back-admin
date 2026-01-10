

using CB.Application.DTOs.Bank;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class BankMappingProfile : MappingProfile
    {
        public BankMappingProfile() : base()
        {
            CreateMap<Bank, BankGetDTO>()
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


            CreateMap<BankCreateDTO, Bank>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<BankEditDTO, Bank>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
