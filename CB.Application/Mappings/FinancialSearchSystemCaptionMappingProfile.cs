
using CB.Application.DTOs.FinancialSearchSystemCaption;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class FinancialSearchSystemCaptionMappingProfile : MappingProfile
    {
        public FinancialSearchSystemCaptionMappingProfile()
        {
            CreateMap<FinancialSearchSystemCaption, FinancialSearchSystemCaptionGetDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<FinancialSearchSystemCaption, FinancialSearchSystemCaptionGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Title
                        )
                    )
                );

            CreateMap<FinancialSearchSystemCaptionPostDTO, FinancialSearchSystemCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
