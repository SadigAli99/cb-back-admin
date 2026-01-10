using AutoMapper;
using CB.Application.DTOs.LotteryFAQ;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class LotteryFAQMappingProfile : Profile
    {
        public LotteryFAQMappingProfile()
        {
            CreateMap<LotteryFAQ, LotteryFAQGetDTO>()
                .ForMember(dest => dest.LotteryTitle, opt => opt.Ignore())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code, t => t.Title ?? string.Empty
                    ));
                })
                .ForMember(dest => dest.Descriptions, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code, t => t.Description ?? string.Empty
                    ));
                });

            CreateMap<LotteryFAQCreateDTO, LotteryFAQ>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<LotteryFAQEditDTO, LotteryFAQ>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
