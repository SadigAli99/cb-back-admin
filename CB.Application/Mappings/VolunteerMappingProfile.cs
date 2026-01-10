

using CB.Application.DTOs.Volunteer;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class VolunteerMappingProfile : MappingProfile
    {
        public VolunteerMappingProfile() : base()
        {
            CreateMap<Volunteer, VolunteerGetDTO>()
                .ForMember(dest => dest.Image, src => src.MapFrom<GenericResolver<Volunteer, VolunteerGetDTO>>())
                .ForMember(dest => dest.Fullnames, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Fullname
                    ));
                })
                .ForMember(dest => dest.Descriptions, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Description
                    ));
                });


            CreateMap<VolunteerCreateDTO, Volunteer>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<VolunteerEditDTO, Volunteer>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
