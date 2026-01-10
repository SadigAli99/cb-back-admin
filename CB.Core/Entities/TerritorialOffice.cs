
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class TerritorialOffice : BaseEntity
    {
        public List<TerritorialOfficeTranslation> Translations { get; set; } = new();
    }
}
