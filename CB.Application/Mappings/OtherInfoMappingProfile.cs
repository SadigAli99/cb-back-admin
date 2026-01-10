

using CB.Application.DTOs.OtherInfo;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class OtherInfoMappingProfile : MappingProfile
    {
        public OtherInfoMappingProfile() : base()
        {
            CreateMap<OtherInfo, OtherInfoGetDTO>()
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<OtherInfoCreateDTO, OtherInfo>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<OtherInfoEditDTO, OtherInfo>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
