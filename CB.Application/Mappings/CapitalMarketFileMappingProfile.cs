

using CB.Application.DTOs.CapitalMarketFile;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CapitalMarketFileMappingProfile : MappingProfile
    {
        public CapitalMarketFileMappingProfile() : base()
        {
            CreateMap<CapitalMarketFile, CapitalMarketFileGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<CapitalMarketFile, CapitalMarketFileGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<CapitalMarketFileCreateDTO, CapitalMarketFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<CapitalMarketFileEditDTO, CapitalMarketFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
