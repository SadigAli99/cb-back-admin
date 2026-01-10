using CB.Application.DTOs.ControlMeasure;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ControlMeasureMappingProfile : MappingProfile
    {
        public ControlMeasureMappingProfile()
        {
            /// <summary>
            /// DTOs For ControlMeasures
            /// </summary>
            /// <typeparam name="ControlMeasure"></typeparam>
            /// <typeparam name="ControlMeasureGetDTO"></typeparam>
            /// <returns></returns>
            CreateMap<ControlMeasure, ControlMeasureGetDTO>()
                .ForMember(dest => dest.ControlMeasureCategoryTitle, src => src.Ignore())
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

            CreateMap<ControlMeasureCreateDTO, ControlMeasure>()
                    .ForMember(dest => dest.Translations, src => src.Ignore());
            CreateMap<ControlMeasureEditDTO, ControlMeasure>()
                    .ForMember(dest => dest.Translations, src => src.Ignore());
        }
    }
}
