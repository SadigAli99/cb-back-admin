

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MonetaryPolicyVideo : BaseEntity
    {
        [StringLength(10000)]
        public string? Url { get; set; }
    }
}
