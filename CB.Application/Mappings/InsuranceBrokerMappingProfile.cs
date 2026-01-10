

using CB.Application.DTOs.InsuranceBroker;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InsuranceBrokerMappingProfile : MappingProfile
    {
        public InsuranceBrokerMappingProfile() : base()
        {
            CreateMap<InsuranceBroker, InsuranceBrokerGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<InsuranceBroker, InsuranceBrokerGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<InsuranceBrokerCreateDTO, InsuranceBroker>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<InsuranceBrokerEditDTO, InsuranceBroker>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
