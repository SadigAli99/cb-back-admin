
using CB.Application.DTOs.AnniversaryStamp;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class AnniversaryStampMappingProfile : MappingProfile
    {
        public AnniversaryStampMappingProfile()
        {
            CreateMap<AnniversaryStamp, AnniversaryStampGetDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<AnniversaryStamp, AnniversaryStampGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Title
                        )
                    )
                );

            CreateMap<AnniversaryStampPostDTO, AnniversaryStamp>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
