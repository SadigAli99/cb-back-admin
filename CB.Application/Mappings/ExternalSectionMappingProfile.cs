

using CB.Application.DTOs.ExternalSection;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ExternalSectionMappingProfile : MappingProfile
    {
        public ExternalSectionMappingProfile() : base()
        {
            CreateMap<ExternalSection, ExternalSectionGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<ExternalSection, ExternalSectionGetDTO>>())
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


            CreateMap<ExternalSectionCreateDTO, ExternalSection>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<ExternalSectionEditDTO, ExternalSection>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
