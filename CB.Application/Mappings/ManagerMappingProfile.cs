

using CB.Application.DTOs.Manager;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ManagerMappingProfile : MappingProfile
    {
        public ManagerMappingProfile() : base()
        {
            CreateMap<Manager, ManagerGetDTO>()
                .ForMember(dest=>dest.Image, src=>src.MapFrom<GenericResolver<Manager,ManagerGetDTO>>())
                .ForMember(dest => dest.Fullnames, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Fullname
                    ));
                })
                .ForMember(dest => dest.Positions, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Position
                    ));
                });


            CreateMap<ManagerCreateDTO, Manager>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<ManagerEditDTO, Manager>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
