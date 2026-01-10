

using CB.Application.DTOs.DigitalPortal;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class DigitalPortalMappingProfile : MappingProfile
    {
        public DigitalPortalMappingProfile() : base()
        {
            CreateMap<DigitalPortal, DigitalPortalGetDTO>()
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.Texts, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Text
                    ));
                });


            CreateMap<DigitalPortalCreateDTO, DigitalPortal>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<DigitalPortalEditDTO, DigitalPortal>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
