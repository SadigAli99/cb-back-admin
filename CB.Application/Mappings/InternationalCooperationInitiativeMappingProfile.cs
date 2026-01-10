

using CB.Application.DTOs.InternationalCooperationInitiative;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InternationalCooperationInitiativeMappingProfile : MappingProfile
    {
        public InternationalCooperationInitiativeMappingProfile() : base()
        {
            CreateMap<InternationalCooperationInitiative, InternationalCooperationInitiativeGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<InternationalCooperationInitiative, InternationalCooperationInitiativeGetDTO>>())
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


            CreateMap<InternationalCooperationInitiativeCreateDTO, InternationalCooperationInitiative>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<InternationalCooperationInitiativeEditDTO, InternationalCooperationInitiative>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
