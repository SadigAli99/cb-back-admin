

using CB.Application.DTOs.CustomerDocumentFile;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CustomerDocumentFileMappingProfile : MappingProfile
    {
        public CustomerDocumentFileMappingProfile() : base()
        {
            CreateMap<CustomerDocumentFile, CustomerDocumentFileGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<CustomerDocumentFile, CustomerDocumentFileGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<CustomerDocumentFileCreateDTO, CustomerDocumentFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<CustomerDocumentFileEditDTO, CustomerDocumentFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
