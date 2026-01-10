

using CB.Application.DTOs.CBAR100Video;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CBAR100VideoMappingProfile : MappingProfile
    {
        public CBAR100VideoMappingProfile() : base()
        {
            CreateMap<CBAR100Video, CBAR100VideoGetDTO>()
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<CBAR100VideoCreateDTO, CBAR100Video>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<CBAR100VideoEditDTO, CBAR100Video>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
