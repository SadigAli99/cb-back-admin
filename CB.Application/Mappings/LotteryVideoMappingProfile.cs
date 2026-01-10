using AutoMapper;
using CB.Application.DTOs.LotteryVideo;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class LotteryVideoMappingProfile : Profile
    {
        public LotteryVideoMappingProfile()
        {
            CreateMap<LotteryVideo, LotteryVideoGetDTO>()
                .ForMember(dest => dest.LotteryTitle, opt => opt.Ignore())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code, t => t.Title ?? string.Empty
                    ));
                });

            CreateMap<LotteryVideoCreateDTO, LotteryVideo>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<LotteryVideoEditDTO, LotteryVideo>()
                .ForMember(dest => dest.Translations, src => src.Ignore());
        }
    }
}
