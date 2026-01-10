

using CB.Application.DTOs.ElectronicMoneyInstitution;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ElectronicMoneyInstitutionMappingProfile : MappingProfile
    {
        public ElectronicMoneyInstitutionMappingProfile() : base()
        {
            CreateMap<ElectronicMoneyInstitution, ElectronicMoneyInstitutionGetDTO>()
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


            CreateMap<ElectronicMoneyInstitutionCreateDTO, ElectronicMoneyInstitution>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<ElectronicMoneyInstitutionEditDTO, ElectronicMoneyInstitution>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
