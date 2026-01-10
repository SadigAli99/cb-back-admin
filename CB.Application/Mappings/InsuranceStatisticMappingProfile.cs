

using CB.Application.DTOs.InsuranceStatistic;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InsuranceStatisticMappingProfile : MappingProfile
    {
        public InsuranceStatisticMappingProfile() : base()
        {
            CreateMap<InsuranceStatistic, InsuranceStatisticGetDTO>()
                .ForMember(dest => dest.InsuranceStatisticCategory, src => src.Ignore())
                .ForMember(dest => dest.InsuranceStatisticSubCategory, src => src.Ignore())
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<InsuranceStatistic, InsuranceStatisticGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<InsuranceStatisticCreateDTO, InsuranceStatistic>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<InsuranceStatisticEditDTO, InsuranceStatistic>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
