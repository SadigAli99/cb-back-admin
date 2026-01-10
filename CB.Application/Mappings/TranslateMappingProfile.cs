
using CB.Application.DTOs.Translate;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class TranslateMappingProfile : MappingProfile
    {
        public TranslateMappingProfile() : base()
        {
            CreateMap<Translate, TranslateGetDTO>()
                .ForMember(dest => dest.Values, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Value
                        )
                    )
                );

            CreateMap<TranslateCreateDTO, Translate>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<TranslateEditDTO, Translate>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
