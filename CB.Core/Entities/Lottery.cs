
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class Lottery : BaseEntity
    {
        public List<LotteryTranslation> Translations { get; set; } = new();
    }
}
