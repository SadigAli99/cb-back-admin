

using CB.Application.DTOs.InformationIssuer;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InformationIssuerMappingProfile : MappingProfile
    {
        public InformationIssuerMappingProfile() : base()
        {
            CreateMap<InformationIssuer, InformationIssuerGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<InformationIssuer, InformationIssuerGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<InformationIssuerCreateDTO, InformationIssuer>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<InformationIssuerEditDTO, InformationIssuer>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
