

using CB.Application.DTOs.LegalActMethodology;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class LegalActMethodologyMappingProfile : MappingProfile
    {
        public LegalActMethodologyMappingProfile() : base()
        {
            CreateMap<LegalActMethodology, LegalActMethodologyGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<LegalActMethodology, LegalActMethodologyGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<LegalActMethodologyCreateDTO, LegalActMethodology>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<LegalActMethodologyEditDTO, LegalActMethodology>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
