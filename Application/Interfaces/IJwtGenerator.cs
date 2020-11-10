using Domain.Auth;

namespace Application.Interfaces
{
  public interface IJwtGenerator
  {
    string CreateToken(WebUser user, int appId);
  }
}
