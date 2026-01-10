using AutoMapper;
using CB.Application.DTOs.FinancialSearchSystem;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class FinancialSearchSystemMappingProfile : Profile
    {
        public FinancialSearchSystemMappingProfile()
        {
            CreateMap<FinancialSearchSystem, FinancialSearchSystemGetDTO>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
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

            CreateMap<FinancialSearchSystemCreateDTO, FinancialSearchSystem>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.Images, src => src.Ignore());

            CreateMap<FinancialSearchSystemEditDTO, FinancialSearchSystem>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.Images, src => src.Ignore());

            CreateMap<FinancialSearchSystemImage, FinancialSearchSystemImageDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<FinancialSearchSystemImage, FinancialSearchSystemImageDTO>>());
        }
    }
}
