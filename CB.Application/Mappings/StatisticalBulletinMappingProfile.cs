

using CB.Application.DTOs.StatisticalBulletin;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class StatisticalBulletinMappingProfile : MappingProfile
    {
        public StatisticalBulletinMappingProfile() : base()
        {
            CreateMap<StatisticalBulletin, StatisticalBulletinGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<StatisticalBulletin, StatisticalBulletinGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<StatisticalBulletinCreateDTO, StatisticalBulletin>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<StatisticalBulletinEditDTO, StatisticalBulletin>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
