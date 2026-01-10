

using CB.Application.DTOs.PaymentSystemControlFile;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class PaymentSystemControlFileMappingProfile : MappingProfile
    {
        public PaymentSystemControlFileMappingProfile() : base()
        {
            CreateMap<PaymentSystemControlFile, PaymentSystemControlFileGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<PaymentSystemControlFile, PaymentSystemControlFileGetDTO>>())
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


            CreateMap<PaymentSystemControlFileCreateDTO, PaymentSystemControlFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<PaymentSystemControlFileEditDTO, PaymentSystemControlFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
