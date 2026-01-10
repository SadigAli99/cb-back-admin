

using CB.Application.DTOs.MoneySignHistory;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MoneySignHistoryMappingProfile : MappingProfile
    {
        public MoneySignHistoryMappingProfile() : base()
        {
            CreateMap<MoneySignHistory, MoneySignHistoryGetDTO>()
                .ForMember(dest => dest.MoneySignTitle, src => src.Ignore())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<MoneySignHistoryCreateDTO, MoneySignHistory>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<MoneySignHistoryEditDTO, MoneySignHistory>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
