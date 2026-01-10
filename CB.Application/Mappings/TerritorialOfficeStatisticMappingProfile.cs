

using CB.Application.DTOs.TerritorialOfficeStatistic;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class TerritorialOfficeStatisticMappingProfile : MappingProfile
    {
        public TerritorialOfficeStatisticMappingProfile() : base()
        {
            CreateMap<TerritorialOfficeStatistic, TerritorialOfficeStatisticGetDTO>()
                .ForMember(dest => dest.TerritorialOfficeTitle, src => src.Ignore())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.Descriptions, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Description
                    ));
                });


            CreateMap<TerritorialOfficeStatisticCreateDTO, TerritorialOfficeStatistic>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<TerritorialOfficeStatisticEditDTO, TerritorialOfficeStatistic>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
