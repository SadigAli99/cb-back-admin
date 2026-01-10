

using CB.Application.DTOs.NakhchivanBulletin;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class NakhchivanBulletinMappingProfile : MappingProfile
    {
        public NakhchivanBulletinMappingProfile() : base()
        {
            CreateMap<NakhchivanBulletin, NakhchivanBulletinGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<NakhchivanBulletin, NakhchivanBulletinGetDTO>>())
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


            CreateMap<NakhchivanBulletinCreateDTO, NakhchivanBulletin>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<NakhchivanBulletinEditDTO, NakhchivanBulletin>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
