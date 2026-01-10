

using CB.Application.DTOs.ReceptionCitizen;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ReceptionCitizenMappingProfile : MappingProfile
    {
        public ReceptionCitizenMappingProfile() : base()
        {
            CreateMap<ReceptionCitizen, ReceptionCitizenGetDTO>()
                .ForMember(dest => dest.ReceptionCitizenCategory, src => src.Ignore())
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


            CreateMap<ReceptionCitizenCreateDTO, ReceptionCitizen>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<ReceptionCitizenEditDTO, ReceptionCitizen>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
