

using CB.Application.DTOs.RealTimeSettlementSystemFile;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class RealTimeSettlementSystemFileMappingProfile : MappingProfile
    {
        public RealTimeSettlementSystemFileMappingProfile() : base()
        {
            CreateMap<RealTimeSettlementSystemFile, RealTimeSettlementSystemFileGetDTO>()
                .ForMember(dest => dest.RealTimeSettlementSystemTitle, src => src.Ignore())
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<RealTimeSettlementSystemFile, RealTimeSettlementSystemFileGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<RealTimeSettlementSystemFileCreateDTO, RealTimeSettlementSystemFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<RealTimeSettlementSystemFileEditDTO, RealTimeSettlementSystemFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
