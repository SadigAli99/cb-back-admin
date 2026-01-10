

using CB.Application.DTOs.NakhchivanPublication;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class NakhchivanPublicationMappingProfile : MappingProfile
    {
        public NakhchivanPublicationMappingProfile() : base()
        {
            CreateMap<NakhchivanPublication, NakhchivanPublicationGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<NakhchivanPublication, NakhchivanPublicationGetDTO>>())
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


            CreateMap<NakhchivanPublicationCreateDTO, NakhchivanPublication>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<NakhchivanPublicationEditDTO, NakhchivanPublication>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
