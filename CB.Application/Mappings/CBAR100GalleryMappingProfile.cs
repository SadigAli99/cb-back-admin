

using CB.Application.DTOs.CBAR100Gallery;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CBAR100GalleryMappingProfile : MappingProfile
    {
        public CBAR100GalleryMappingProfile() : base()
        {
            CreateMap<CBAR100Gallery, CBAR100GalleryGetDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<CBAR100Gallery, CBAR100GalleryGetDTO>>());


            CreateMap<CBAR100GalleryCreateDTO, CBAR100Gallery>();

            CreateMap<CBAR100GalleryEditDTO, CBAR100Gallery>();

        }
    }
}
