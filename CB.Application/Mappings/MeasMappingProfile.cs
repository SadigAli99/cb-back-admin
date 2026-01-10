

using CB.Application.DTOs.Meas;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MeasMappingProfile : MappingProfile
    {
        public MeasMappingProfile() : base()
        {
            CreateMap<Meas, MeasGetDTO>()
                .ForMember(dest => dest.PdfFile, opt => opt.MapFrom<GenericResolver<Meas, MeasGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<MeasCreateDTO, Meas>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.PdfFile, src => src.Ignore());

            CreateMap<MeasEditDTO, Meas>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.PdfFile, src => src.Ignore());

        }
    }
}
