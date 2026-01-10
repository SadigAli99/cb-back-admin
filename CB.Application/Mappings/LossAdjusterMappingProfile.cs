

using CB.Application.DTOs.LossAdjuster;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class LossAdjusterMappingProfile : MappingProfile
    {
        public LossAdjusterMappingProfile() : base()
        {
            CreateMap<LossAdjuster, LossAdjusterGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<LossAdjuster, LossAdjusterGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<LossAdjusterCreateDTO, LossAdjuster>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<LossAdjusterEditDTO, LossAdjuster>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
