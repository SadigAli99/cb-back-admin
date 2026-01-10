

using CB.Application.DTOs.InsurerPresidentAct;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InsurerPresidentActMappingProfile : MappingProfile
    {
        public InsurerPresidentActMappingProfile() : base()
        {
            CreateMap<InsurerPresidentAct, InsurerPresidentActGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<InsurerPresidentAct, InsurerPresidentActGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<InsurerPresidentActCreateDTO, InsurerPresidentAct>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<InsurerPresidentActEditDTO, InsurerPresidentAct>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
