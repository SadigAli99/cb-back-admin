

using CB.Application.DTOs.ElectronicMoneyInstitutionFile;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ElectronicMoneyInstitutionFileMappingProfile : MappingProfile
    {
        public ElectronicMoneyInstitutionFileMappingProfile() : base()
        {
            CreateMap<ElectronicMoneyInstitutionFile, ElectronicMoneyInstitutionFileGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<ElectronicMoneyInstitutionFile, ElectronicMoneyInstitutionFileGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.CoverTitles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.CoverTitle
                    ));
                });


            CreateMap<ElectronicMoneyInstitutionFileCreateDTO, ElectronicMoneyInstitutionFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<ElectronicMoneyInstitutionFileEditDTO, ElectronicMoneyInstitutionFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
