

using CB.Application.DTOs.Insurer;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InsurerMappingProfile : MappingProfile
    {
        public InsurerMappingProfile() : base()
        {
            CreateMap<Insurer, InsurerGetDTO>()
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.Descriptions, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Description
                    ));
                });


            CreateMap<InsurerCreateDTO, Insurer>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<InsurerEditDTO, Insurer>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
