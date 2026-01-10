using AutoMapper;
using CB.Application.DTOs.VirtualEducation;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class VirtualEducationMappingProfile : Profile
    {
        public VirtualEducationMappingProfile()
        {
            CreateMap<VirtualEducation, VirtualEducationGetDTO>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code, t => t.Title ?? string.Empty
                    ));
                })
                .ForMember(dest => dest.Descriptions, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code, t => t.Description ?? string.Empty
                    ));
                });

            CreateMap<VirtualEducationCreateDTO, VirtualEducation>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.Images, src => src.Ignore());

            CreateMap<VirtualEducationEditDTO, VirtualEducation>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.Images, src => src.Ignore());

            CreateMap<VirtualEducationImage, VirtualEducationImageDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<VirtualEducationImage, VirtualEducationImageDTO>>());
        }
    }
}
