

using CB.Application.DTOs.LegalAct;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class LegalActMappingProfile : MappingProfile
    {
        public LegalActMappingProfile() : base()
        {
            CreateMap<LegalAct, LegalActGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<LegalAct, LegalActGetDTO>>())
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


            CreateMap<LegalActCreateDTO, LegalAct>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<LegalActEditDTO, LegalAct>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
