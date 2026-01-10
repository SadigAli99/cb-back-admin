
using CB.Application.DTOs.QualificationCertificateCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class QualificationCertificateCaptionMappingProfile : MappingProfile
    {
        public QualificationCertificateCaptionMappingProfile()
        {
            CreateMap<QualificationCertificateCaption, QualificationCertificateCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<QualificationCertificateCaptionPostDTO, QualificationCertificateCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
