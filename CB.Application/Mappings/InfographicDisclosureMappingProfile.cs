using CB.Application.DTOs.InfographicDisclosure;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InfographicDisclosureMappingProfile : MappingProfile
    {
        public InfographicDisclosureMappingProfile() : base()
        {
            CreateMap<InfographicDisclosure, InfographicDisclosureGetDTO>()
                .ForMember(dest => dest.InfographicDisclosureCategory, src => src.Ignore())
                .ForMember(dest => dest.InfographicDisclosureFrequency, src => src.Ignore())
                .ForMember(dest => dest.Deadlines, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code ?? "az",
                        t => t.Deadline
                    ));
                })
                .ForMember(dest => dest.Descriptions, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Description
                    ));
                });


            CreateMap<InfographicDisclosureCreateDTO, InfographicDisclosure>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<InfographicDisclosureEditDTO, InfographicDisclosure>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
