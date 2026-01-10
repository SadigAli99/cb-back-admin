

using CB.Application.DTOs.OtherRight;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class OtherRightMappingProfile : MappingProfile
    {
        public OtherRightMappingProfile() : base()
        {
            CreateMap<OtherRight, OtherRightGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<OtherRight, OtherRightGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<OtherRightCreateDTO, OtherRight>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<OtherRightEditDTO, OtherRight>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
