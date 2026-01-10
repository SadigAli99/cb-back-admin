
using CB.Application.DTOs.Poster;
using CB.Core.Entities;
using CB.Application.Mappings.Resolvers;

namespace CB.Application.Mappings
{
    public class PosterMappingProfile : MappingProfile
    {
        public PosterMappingProfile()
        {
            CreateMap<Poster, PosterGetDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<Poster, PosterGetDTO>>());
            CreateMap<PosterCreateDTO, Poster>();
            CreateMap<PosterEditDTO, Poster>();
        }
    }
}
