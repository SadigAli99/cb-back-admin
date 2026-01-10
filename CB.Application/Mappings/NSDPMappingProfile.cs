

using CB.Application.DTOs.NSDP;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class NSDPMappingProfile : MappingProfile
    {
        public NSDPMappingProfile() : base()
        {
            CreateMap<NSDP, NSDPGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<NSDP, NSDPGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<NSDPCreateDTO, NSDP>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<NSDPEditDTO, NSDP>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
