
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class DigitalPortalCaption : BaseEntity
    {
        public List<DigitalPortalCaptionTranslation>Translations {get; set; } = new();
    }
}
