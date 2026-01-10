

using CB.Application.DTOs.PaymentSystemOperationFile;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class PaymentSystemOperationFileMappingProfile : MappingProfile
    {
        public PaymentSystemOperationFileMappingProfile() : base()
        {
            CreateMap<PaymentSystemOperationFile, PaymentSystemOperationFileGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<PaymentSystemOperationFile, PaymentSystemOperationFileGetDTO>>())
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


            CreateMap<PaymentSystemOperationFileCreateDTO, PaymentSystemOperationFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<PaymentSystemOperationFileEditDTO, PaymentSystemOperationFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
