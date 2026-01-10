

using CB.Application.DTOs.ForeignInsuranceBroker;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ForeignInsuranceBrokerMappingProfile : MappingProfile
    {
        public ForeignInsuranceBrokerMappingProfile() : base()
        {
            CreateMap<ForeignInsuranceBroker, ForeignInsuranceBrokerGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<ForeignInsuranceBroker, ForeignInsuranceBrokerGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<ForeignInsuranceBrokerCreateDTO, ForeignInsuranceBroker>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<ForeignInsuranceBrokerEditDTO, ForeignInsuranceBroker>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
