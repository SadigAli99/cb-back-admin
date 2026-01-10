using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CB.Infrastructure.Persistance.Configurations
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasData(
                new RolePermission { Id = 1, RoleId = 1, PermissionId = 1 },
                new RolePermission { Id = 2, RoleId = 1, PermissionId = 2 },
                new RolePermission { Id = 3, RoleId = 1, PermissionId = 3 },
                new RolePermission { Id = 4, RoleId = 1, PermissionId = 4 },

                new RolePermission { Id = 5, RoleId = 2, PermissionId = 1 },
                new RolePermission { Id = 6, RoleId = 2, PermissionId = 2 },
                new RolePermission { Id = 7, RoleId = 2, PermissionId = 3 },

                new RolePermission { Id = 8, RoleId = 3, PermissionId = 1 },
                new RolePermission { Id = 9, RoleId = 3, PermissionId = 2 }
            );
        }
    }
}
