
using CB.Application.DTOs.FinancialDevelopment;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class FinancialDevelopmentMappingProfile : MappingProfile
    {
        public FinancialDevelopmentMappingProfile()
        {
            CreateMap<FinancialDevelopment, FinancialDevelopmentGetDTO>()
                .ForMember(dest => dest.PdfFile, opt => opt.MapFrom<GenericResolver<FinancialDevelopment, FinancialDevelopmentGetDTO>>())
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<FinancialDevelopment, FinancialDevelopmentGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Title
                        )
                    )
                )
                .ForMember(dest => dest.FileHeadTitles, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.FileHeadTitle
                        )
                    )
                )
                .ForMember(dest => dest.FileTitles, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.FileTitle
                        )
                    )
                )
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<FinancialDevelopmentPostDTO, FinancialDevelopment>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
