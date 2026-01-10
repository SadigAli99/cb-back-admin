

using CB.Application.DTOs.CustomerFeedback;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CustomerFeedbackMappingProfile : MappingProfile
    {
        public CustomerFeedbackMappingProfile() : base()
        {
            CreateMap<CustomerFeedback, CustomerFeedbackGetDTO>()
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<CustomerFeedbackCreateDTO, CustomerFeedback>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<CustomerFeedbackEditDTO, CustomerFeedback>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
