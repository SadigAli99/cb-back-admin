

using CB.Application.DTOs.MicrofinanceModel;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MicrofinanceModelMappingProfile : MappingProfile
    {
        public MicrofinanceModelMappingProfile() : base()
        {
            CreateMap<MicrofinanceModel, MicrofinanceModelGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<MicrofinanceModel, MicrofinanceModelGetDTO>>())
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


            CreateMap<MicrofinanceModelCreateDTO, MicrofinanceModel>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<MicrofinanceModelEditDTO, MicrofinanceModel>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
