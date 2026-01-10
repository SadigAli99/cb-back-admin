

using CB.Application.DTOs.Director;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class DirectorMappingProfile : MappingProfile
    {
        public DirectorMappingProfile() : base()
        {
            CreateMap<Director, DirectorGetDTO>()
                .ForMember(dest=>dest.Image, src=>src.MapFrom<GenericResolver<Director,DirectorGetDTO>>())
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


            CreateMap<DirectorCreateDTO, Director>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<DirectorEditDTO, Director>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
