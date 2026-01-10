

using CB.Application.DTOs.FinancingActivity;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class FinancingActivityMappingProfile : MappingProfile
    {
        public FinancingActivityMappingProfile() : base()
        {
            CreateMap<FinancingActivity, FinancingActivityGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<FinancingActivity, FinancingActivityGetDTO>>())
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


            CreateMap<FinancingActivityCreateDTO, FinancingActivity>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<FinancingActivityEditDTO, FinancingActivity>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
