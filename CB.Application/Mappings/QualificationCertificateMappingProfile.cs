

using CB.Application.DTOs.QualificationCertificate;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class QualificationCertificateMappingProfile : MappingProfile
    {
        public QualificationCertificateMappingProfile() : base()
        {
            CreateMap<QualificationCertificate, QualificationCertificateGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<QualificationCertificate, QualificationCertificateGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<QualificationCertificateCreateDTO, QualificationCertificate>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<QualificationCertificateEditDTO, QualificationCertificate>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
