
using CB.Application.DTOs.AnniversaryCoin;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class AnniversaryCoinMappingProfile : MappingProfile
    {
        public AnniversaryCoinMappingProfile()
        {
            CreateMap<AnniversaryCoin, AnniversaryCoinGetDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<AnniversaryCoin, AnniversaryCoinGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Title
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

            CreateMap<AnniversaryCoinPostDTO, AnniversaryCoin>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
