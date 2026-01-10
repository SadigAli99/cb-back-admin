using CB.Application.Interfaces.Repositories;
using CB.Core.Entities;
using CB.Infrastructure.Persistance;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }


        public async Task<User?> AuthenticateAsync(string email, string password)
        {
            var user = await _context.Users
                        .Include(x => x.Role)
                        .FirstOrDefaultAsync(x => x.Email == email);
            if (user == null) return null;

            var result = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!result) return null;

            return user;
        }

        public async Task<User?> GetByIdWithRolePermissionsAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Role)
                    .ThenInclude(r => r.Permissions) // Role → RolePermission list
                    .ThenInclude(rp => rp.Permission)   // RolePermission → Permission
                    .FirstOrDefaultAsync(u => u.Id == id);
        }


        public async Task<bool> LogoutAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return false;

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;

            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<bool> UpdateAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
