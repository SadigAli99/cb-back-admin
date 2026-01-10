

using CB.Application.DTOs.InformationMemorandum;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InformationMemorandumMappingProfile : MappingProfile
    {
        public InformationMemorandumMappingProfile() : base()
        {
            CreateMap<InformationMemorandum, InformationMemorandumGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<InformationMemorandum, InformationMemorandumGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<InformationMemorandumCreateDTO, InformationMemorandum>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<InformationMemorandumEditDTO, InformationMemorandum>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
