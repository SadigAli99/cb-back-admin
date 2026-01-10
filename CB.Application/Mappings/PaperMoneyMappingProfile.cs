

using CB.Application.DTOs.PaperMoney;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class PaperMoneyMappingProfile : MappingProfile
    {
        public PaperMoneyMappingProfile() : base()
        {
            CreateMap<Money, PaperMoneyGetDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<Money, PaperMoneyGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.Topics, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Topic
                    ));
                })
                .ForMember(dest => dest.ReleaseDates, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.ReleaseDate
                    ));
                });


            CreateMap<PaperMoneyCreateDTO, Money>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<PaperMoneyEditDTO, Money>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
