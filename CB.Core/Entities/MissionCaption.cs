
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MissionCaption : BaseEntity
    {
        public List<MissionCaptionTranslation>? Translations { get; set; }
    }
}
