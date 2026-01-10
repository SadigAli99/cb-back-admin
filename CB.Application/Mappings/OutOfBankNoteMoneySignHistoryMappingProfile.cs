

using CB.Application.DTOs.OutOfBankNoteMoneySignHistory;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class OutOfBankNoteMoneySignHistoryMappingProfile : MappingProfile
    {
        public OutOfBankNoteMoneySignHistoryMappingProfile() : base()
        {
            CreateMap<MoneySignHistory, OutOfBankNoteMoneySignHistoryGetDTO>()
                .ForMember(dest => dest.MoneySignTitle, src => src.Ignore())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<OutOfBankNoteMoneySignHistoryCreateDTO, MoneySignHistory>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<OutOfBankNoteMoneySignHistoryEditDTO, MoneySignHistory>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
