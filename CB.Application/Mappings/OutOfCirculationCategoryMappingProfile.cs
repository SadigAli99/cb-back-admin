

using CB.Application.DTOs.OutOfCirculationCategory;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;
using CB.Core.Enums;

namespace CB.Application.Mappings
{
    public class OutOfCirculationCategoryMappingProfile : MappingProfile
    {
        public OutOfCirculationCategoryMappingProfile() : base()
        {
            CreateMap<OutOfCirculationCategory, OutOfCirculationCategoryGetDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<OutOfCirculationCategory, OutOfCirculationCategoryGetDTO>>())
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type == MoneyType.PAPER ? "Kağız" : "Metal"))
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<OutOfCirculationCategoryCreateDTO, OutOfCirculationCategory>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<OutOfCirculationCategoryEditDTO, OutOfCirculationCategory>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
