using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CB.Core.Entities;

namespace CB.Application.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        Task<User?> GetUserByRefreshTokenAsync(string refreshToken);
        Task<User?> AuthenticateAsync(string email, string password);
        Task<User?> GetByIdWithRolePermissionsAsync(int id);
        Task<bool> UpdateAsync();
        Task<bool> LogoutAsync(int userId);
    }
}
