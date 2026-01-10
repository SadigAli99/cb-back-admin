
using System.Security.Claims;
using CB.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace CB.Infrastructure.Auth
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IAuthRepository _authRepository;

        public PermissionHandler(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return;

            if (!int.TryParse(userIdClaim, out var userId))
                return;

            var user = await _authRepository.GetByIdWithRolePermissionsAsync(userId);
            if (user == null) return;

            var rolePermissions = user.Role?.Permissions?.Select(rp => rp.Permission?.Name).ToList();
            if (rolePermissions != null && rolePermissions.Contains(requirement.Permission))
            {
                context.Succeed(requirement);
            }
        }
    }
}
