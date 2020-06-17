using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace Infrastructure.Security
{
  public class UserAccessor : IUserAccessor
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserAccessor(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public string GetCurrentUsername()
    {
      var username = _httpContextAccessor.HttpContext.User?.Claims
        ?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

      return username;
    }

    public string GetCurrentUserRole()
    {
      var role = _httpContextAccessor.HttpContext.User?.Claims
        ?.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

      return role;
    }

    public long GetCurrentRoleId()
    {
      var roleIdClaim = _httpContextAccessor.HttpContext.User?.Claims
        ?.FirstOrDefault(x => x.Type == "roleId")?.Value;

      long.TryParse(roleIdClaim, out var roleId);

      return roleId;
    }

    public long GetCurrentAppId()
    {
      var appIdClaim = _httpContextAccessor.HttpContext.User?.Claims
        ?.FirstOrDefault(x => x.Type == "appId")?.Value;

      long.TryParse(appIdClaim, out var appId);

      return appId;
    }
  }
}
