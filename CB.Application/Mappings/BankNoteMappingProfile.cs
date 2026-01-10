using CB.Application.DTOs.BankSector;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class BankSectorMappingProfile : MappingProfile
    {
        public BankSectorMappingProfile()
        {
            /// <summary>
            /// DTOs For BankSectors
            /// </summary>
            /// <typeparam name="BankSector"></typeparam>
            /// <typeparam name="BankSectorGetDTO"></typeparam>
            /// <returns></returns>
            CreateMap<BankSector, BankSectorGetDTO>()
                .ForMember(x => x.PercentCategoryTitle, src => src.Ignore());
            CreateMap<BankSectorCreateDTO, BankSector>();
            CreateMap<BankSectorCreateDTO, BankSectorEditDTO>();
            CreateMap<BankSectorEditDTO, BankSector>().ReverseMap();
        }
    }
}
