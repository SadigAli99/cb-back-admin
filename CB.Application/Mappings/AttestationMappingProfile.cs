

using CB.Application.DTOs.Attestation;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class AttestationMappingProfile : MappingProfile
    {
        public AttestationMappingProfile() : base()
        {
            CreateMap<Attestation, AttestationGetDTO>()
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


            CreateMap<AttestationCreateDTO, Attestation>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<AttestationEditDTO, Attestation>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
