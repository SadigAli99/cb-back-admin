

using CB.Application.DTOs.InsurerRight;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InsurerRightMappingProfile : MappingProfile
    {
        public InsurerRightMappingProfile() : base()
        {
            CreateMap<InsurerRight, InsurerRightGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<InsurerRight, InsurerRightGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<InsurerRightCreateDTO, InsurerRight>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<InsurerRightEditDTO, InsurerRight>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
