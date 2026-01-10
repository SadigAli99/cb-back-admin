using AutoMapper;
using CB.Application.DTOs.FinancialLiteracyEvent;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class FinancialLiteracyEventMappingProfile : Profile
    {
        public FinancialLiteracyEventMappingProfile()
        {
            CreateMap<FinancialLiteracyEvent, FinancialLiteracyEventGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code, t => t.Description ?? string.Empty
                    ));
                });

            CreateMap<FinancialLiteracyEventCreateDTO, FinancialLiteracyEvent>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<FinancialLiteracyEventEditDTO, FinancialLiteracyEvent>()
                .ForMember(dest => dest.Translations, src => src.Ignore());
        }
    }
}
