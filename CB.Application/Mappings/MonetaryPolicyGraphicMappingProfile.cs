

using CB.Application.DTOs.MonetaryPolicyGraphic;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MonetaryPolicyGraphicMappingProfile : MappingProfile
    {
        public MonetaryPolicyGraphicMappingProfile() : base()
        {
            CreateMap<MonetaryPolicyGraphic, MonetaryPolicyGraphicGetDTO>()
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


            CreateMap<MonetaryPolicyGraphicCreateDTO, MonetaryPolicyGraphic>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<MonetaryPolicyGraphicEditDTO, MonetaryPolicyGraphic>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
