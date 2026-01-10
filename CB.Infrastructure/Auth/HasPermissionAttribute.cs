
using Microsoft.AspNetCore.Authorization;

namespace CB.Infrastructure.Auth
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(string permission)
        {
            Policy = permission;
        }
    }
}
