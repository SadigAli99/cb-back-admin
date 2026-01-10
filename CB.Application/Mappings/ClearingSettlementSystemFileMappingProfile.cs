

using CB.Application.DTOs.ClearingSettlementSystemFile;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ClearingSettlementSystemFileMappingProfile : MappingProfile
    {
        public ClearingSettlementSystemFileMappingProfile() : base()
        {
            CreateMap<ClearingSettlementSystemFile, ClearingSettlementSystemFileGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<ClearingSettlementSystemFile, ClearingSettlementSystemFileGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<ClearingSettlementSystemFileCreateDTO, ClearingSettlementSystemFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<ClearingSettlementSystemFileEditDTO, ClearingSettlementSystemFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
