

using CB.Application.DTOs.TerritorialOfficeRegion;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class TerritorialOfficeRegionMappingProfile : MappingProfile
    {
        public TerritorialOfficeRegionMappingProfile() : base()
        {
            CreateMap<TerritorialOfficeRegion, TerritorialOfficeRegionGetDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<TerritorialOfficeRegion, TerritorialOfficeRegionGetDTO>>())
                .ForMember(dest => dest.TerritorialOfficeTitle, src => src.Ignore())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.Directors, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Director
                    ));
                })
                .ForMember(dest => dest.Locations, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Location
                    ));
                });


            CreateMap<TerritorialOfficeRegionCreateDTO, TerritorialOfficeRegion>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<TerritorialOfficeRegionEditDTO, TerritorialOfficeRegion>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
