using CB.Application.DTOs.BankNote;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class BankNoteMappingProfile : MappingProfile
    {
        public BankNoteMappingProfile()
        {
            /// <summary>
            /// DTOs For BankNotes
            /// </summary>
            /// <typeparam name="BankNote"></typeparam>
            /// <typeparam name="BankNoteGetDTO"></typeparam>
            /// <returns></returns>
            CreateMap<BankNote, BankNoteGetDTO>()
                .ForMember(x => x.PercentCategoryTitle, src => src.Ignore());
            CreateMap<BankNoteCreateDTO, BankNote>();
            CreateMap<BankNoteCreateDTO, BankNoteEditDTO>();
            CreateMap<BankNoteEditDTO, BankNote>().ReverseMap();
        }
    }
}
