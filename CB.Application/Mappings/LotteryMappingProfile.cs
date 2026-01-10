

using CB.Application.DTOs.Lottery;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class LotteryMappingProfile : MappingProfile
    {
        public LotteryMappingProfile() : base()
        {
            CreateMap<Lottery, LotteryGetDTO>()
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.Slugs, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Slug
                    ));
                })
                .ForMember(dest => dest.Descriptions, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Description
                    ));
                });


            CreateMap<LotteryCreateDTO, Lottery>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<LotteryEditDTO, Lottery>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
