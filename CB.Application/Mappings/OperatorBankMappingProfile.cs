

using CB.Application.DTOs.OperatorBank;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class OperatorBankMappingProfile : MappingProfile
    {
        public OperatorBankMappingProfile() : base()
        {
            CreateMap<OperatorBank, OperatorBankGetDTO>()
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<OperatorBankCreateDTO, OperatorBank>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<OperatorBankEditDTO, OperatorBank>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
