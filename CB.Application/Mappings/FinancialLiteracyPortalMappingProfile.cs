using AutoMapper;
using CB.Application.DTOs.FinancialLiteracyPortal;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class FinancialLiteracyPortalMappingProfile : Profile
    {
        public FinancialLiteracyPortalMappingProfile()
        {
            CreateMap<FinancialLiteracyPortal, FinancialLiteracyPortalGetDTO>()
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

            CreateMap<FinancialLiteracyPortalCreateDTO, FinancialLiteracyPortal>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.Images, src => src.Ignore());

            CreateMap<FinancialLiteracyPortalEditDTO, FinancialLiteracyPortal>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.Images, src => src.Ignore());

            CreateMap<FinancialLiteracyPortalImage, FinancialLiteracyPortalImageDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<FinancialLiteracyPortalImage, FinancialLiteracyPortalImageDTO>>());
        }
    }
}
