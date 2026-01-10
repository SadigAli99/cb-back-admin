
using CB.Application.DTOs.ElectronicMoneyInstitutionCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ElectronicMoneyInstitutionCaptionMappingProfile : MappingProfile
    {
        public ElectronicMoneyInstitutionCaptionMappingProfile()
        {
            CreateMap<ElectronicMoneyInstitutionCaption, ElectronicMoneyInstitutionCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<ElectronicMoneyInstitutionCaptionPostDTO, ElectronicMoneyInstitutionCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
