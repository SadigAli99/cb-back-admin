using CB.Application.DTOs.Faq;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class FaqMappingProfile : MappingProfile
    {
        public FaqMappingProfile() : base()
        {
            CreateMap<Faq, FaqGetDTO>()
                .ForMember(dest => dest.FaqCategory, src => src.Ignore())
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


            CreateMap<FaqCreateDTO, Faq>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<FaqEditDTO, Faq>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
