

using CB.Application.DTOs.CreditInstitutionRight;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CreditInstitutionRightMappingProfile : MappingProfile
    {
        public CreditInstitutionRightMappingProfile() : base()
        {
            CreateMap<CreditInstitutionRight, CreditInstitutionRightGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<CreditInstitutionRight, CreditInstitutionRightGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<CreditInstitutionRightCreateDTO, CreditInstitutionRight>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<CreditInstitutionRightEditDTO, CreditInstitutionRight>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
