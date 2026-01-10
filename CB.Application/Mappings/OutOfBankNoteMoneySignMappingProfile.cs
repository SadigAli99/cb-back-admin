

using CB.Application.DTOs.OutOfBankNoteMoneySign;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class OutOfBankNoteMoneySignMappingProfile : MappingProfile
    {
        public OutOfBankNoteMoneySignMappingProfile() : base()
        {
            CreateMap<MoneySign, OutOfBankNoteMoneySignGetDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<MoneySign, OutOfBankNoteMoneySignGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<OutOfBankNoteMoneySignCreateDTO, MoneySign>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<OutOfBankNoteMoneySignEditDTO, MoneySign>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
