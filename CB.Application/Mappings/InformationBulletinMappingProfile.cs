

using CB.Application.DTOs.InformationBulletin;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InformationBulletinMappingProfile : MappingProfile
    {
        public InformationBulletinMappingProfile() : base()
        {
            CreateMap<InformationBulletin, InformationBulletinGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<InformationBulletin, InformationBulletinGetDTO>>())
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


            CreateMap<InformationBulletinCreateDTO, InformationBulletin>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<InformationBulletinEditDTO, InformationBulletin>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
