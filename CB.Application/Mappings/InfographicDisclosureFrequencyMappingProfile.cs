
using CB.Application.DTOs.InfographicDisclosureFrequency;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InfographicDisclosureFrequencyMappingProfile : MappingProfile
    {
        public InfographicDisclosureFrequencyMappingProfile() : base()
        {
            CreateMap<InfographicDisclosureFrequency, InfographicDisclosureFrequencyGetDTO>()
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    );

            CreateMap<InfographicDisclosureFrequencyCreateDTO, InfographicDisclosureFrequency>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<InfographicDisclosureFrequencyEditDTO, InfographicDisclosureFrequency>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
