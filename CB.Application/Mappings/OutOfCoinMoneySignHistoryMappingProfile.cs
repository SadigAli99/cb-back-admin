

using CB.Application.DTOs.OutOfCoinMoneySignHistory;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class OutOfCoinMoneySignHistoryMappingProfile : MappingProfile
    {
        public OutOfCoinMoneySignHistoryMappingProfile() : base()
        {
            CreateMap<MoneySignHistory, OutOfCoinMoneySignHistoryGetDTO>()
                .ForMember(dest => dest.MoneySignTitle, src => src.Ignore())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<OutOfCoinMoneySignHistoryCreateDTO, MoneySignHistory>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<OutOfCoinMoneySignHistoryEditDTO, MoneySignHistory>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
