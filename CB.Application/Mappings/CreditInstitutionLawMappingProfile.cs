

using CB.Application.DTOs.CreditInstitutionLaw;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CreditInstitutionLawMappingProfile : MappingProfile
    {
        public CreditInstitutionLawMappingProfile() : base()
        {
            CreateMap<CreditInstitutionLaw, CreditInstitutionLawGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<CreditInstitutionLaw, CreditInstitutionLawGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<CreditInstitutionLawCreateDTO, CreditInstitutionLaw>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<CreditInstitutionLawEditDTO, CreditInstitutionLaw>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
