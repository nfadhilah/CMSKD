using Domain;

namespace Application.Interfaces
{
  public interface IJwtGenerator
  {
    string CreateToken(WebUser user);
  }
}
