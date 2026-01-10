

using CB.Application.DTOs.BankInvestmentFile;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class BankInvestmentFileMappingProfile : MappingProfile
    {
        public BankInvestmentFileMappingProfile() : base()
        {
            CreateMap<BankInvestmentFile, BankInvestmentFileGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<BankInvestmentFile, BankInvestmentFileGetDTO>>())
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


            CreateMap<BankInvestmentFileCreateDTO, BankInvestmentFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<BankInvestmentFileEditDTO, BankInvestmentFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
