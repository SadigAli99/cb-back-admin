

using CB.Application.DTOs.NationalBankNoteMoneySign;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class NationalBankNoteMoneySignMappingProfile : MappingProfile
    {
        public NationalBankNoteMoneySignMappingProfile() : base()
        {
            CreateMap<MoneySign, NationalBankNoteMoneySignGetDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<MoneySign, NationalBankNoteMoneySignGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<NationalBankNoteMoneySignCreateDTO, MoneySign>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<NationalBankNoteMoneySignEditDTO, MoneySign>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
