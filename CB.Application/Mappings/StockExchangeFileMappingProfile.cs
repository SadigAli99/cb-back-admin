

using CB.Application.DTOs.StockExchangeFile;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class StockExchangeFileMappingProfile : MappingProfile
    {
        public StockExchangeFileMappingProfile() : base()
        {
            CreateMap<StockExchangeFile, StockExchangeFileGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<StockExchangeFile, StockExchangeFileGetDTO>>())
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


            CreateMap<StockExchangeFileCreateDTO, StockExchangeFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<StockExchangeFileEditDTO, StockExchangeFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
