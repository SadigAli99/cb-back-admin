
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CentralBankCooperation : BaseEntity
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public List<CentralBankCooperationTranslation> Translations { get; set; } = new();
    }
}
