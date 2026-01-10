

using CB.Application.DTOs.CreditInstitution;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CreditInstitutionMappingProfile : MappingProfile
    {
        public CreditInstitutionMappingProfile() : base()
        {
            CreateMap<CreditInstitution, CreditInstitutionGetDTO>()
                .ForMember(dest => dest.CreditInstitutionCategory, src => src.Ignore())
                .ForMember(dest => dest.CreditInstitutionSubCategory, src => src.Ignore())
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<CreditInstitution, CreditInstitutionGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<CreditInstitutionCreateDTO, CreditInstitution>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<CreditInstitutionEditDTO, CreditInstitution>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
