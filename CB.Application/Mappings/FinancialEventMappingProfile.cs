using AutoMapper;
using CB.Application.DTOs.FinancialEvent;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class FinancialEventMappingProfile : Profile
    {
        public FinancialEventMappingProfile()
        {
            CreateMap<FinancialEvent, FinancialEventGetDTO>()
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code, t => t.Title ?? string.Empty
                    ));
                })
                .ForMember(dest => dest.Slugs, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code, t => t.Slug ?? string.Empty
                    ));
                })
                .ForMember(dest => dest.Descriptions, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code, t => t.Description ?? string.Empty
                    ));
                });

            CreateMap<FinancialEventCreateDTO, FinancialEvent>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<FinancialEventEditDTO, FinancialEvent>()
                .ForMember(dest => dest.Translations, src => src.Ignore());
        }
    }
}
