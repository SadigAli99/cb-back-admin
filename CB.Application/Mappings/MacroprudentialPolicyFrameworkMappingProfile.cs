

using CB.Application.DTOs.MacroprudentialPolicyFramework;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MacroprudentialPolicyFrameworkMappingProfile : MappingProfile
    {
        public MacroprudentialPolicyFrameworkMappingProfile() : base()
        {
            CreateMap<MacroprudentialPolicyFramework, MacroprudentialPolicyFrameworkGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<MacroprudentialPolicyFramework, MacroprudentialPolicyFrameworkGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.CoverTitles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.CoverTitle
                    ));
                });


            CreateMap<MacroprudentialPolicyFrameworkCreateDTO, MacroprudentialPolicyFramework>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<MacroprudentialPolicyFrameworkEditDTO, MacroprudentialPolicyFramework>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
