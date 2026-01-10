

using CB.Application.DTOs.Office;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class OfficeMappingProfile : MappingProfile
    {
        public OfficeMappingProfile() : base()
        {
            CreateMap<Office, OfficeGetDTO>()
                .ForMember(dest => dest.Image, src => src.MapFrom<GenericResolver<Office, OfficeGetDTO>>())
                .ForMember(dest => dest.Statute, src => src.MapFrom<GenericResolver<Office, OfficeGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.Chairmen, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Chairman
                    ));
                })
                .ForMember(dest => dest.Addresses, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Address
                    ));
                });


            CreateMap<OfficeCreateDTO, Office>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<OfficeEditDTO, Office>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
