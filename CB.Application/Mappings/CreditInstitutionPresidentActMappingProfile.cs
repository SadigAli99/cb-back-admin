

using CB.Application.DTOs.CreditInstitutionPresidentAct;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CreditInstitutionPresidentActMappingProfile : MappingProfile
    {
        public CreditInstitutionPresidentActMappingProfile() : base()
        {
            CreateMap<CreditInstitutionPresidentAct, CreditInstitutionPresidentActGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<CreditInstitutionPresidentAct, CreditInstitutionPresidentActGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<CreditInstitutionPresidentActCreateDTO, CreditInstitutionPresidentAct>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<CreditInstitutionPresidentActEditDTO, CreditInstitutionPresidentAct>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
