using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CB.Core.Entities
{
    public class User : BaseEntity
    {
        [MaxLength(50)]
        public string Username { get; set; } = null!;

        [MaxLength(100)]
        public string Email { get; set; } = null!;

        [MaxLength(255)]
        public string PasswordHash { get; set; } = null!;
        public string? RefreshToken { get; set; } = null;
        public DateTime? RefreshTokenExpiryTime { get; set; } = null;
        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;
    }
}
