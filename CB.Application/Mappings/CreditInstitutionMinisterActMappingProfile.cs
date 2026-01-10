

using CB.Application.DTOs.CreditInstitutionMinisterAct;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CreditInstitutionMinisterActMappingProfile : MappingProfile
    {
        public CreditInstitutionMinisterActMappingProfile() : base()
        {
            CreateMap<CreditInstitutionMinisterAct, CreditInstitutionMinisterActGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<CreditInstitutionMinisterAct, CreditInstitutionMinisterActGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<CreditInstitutionMinisterActCreateDTO, CreditInstitutionMinisterAct>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<CreditInstitutionMinisterActEditDTO, CreditInstitutionMinisterAct>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
