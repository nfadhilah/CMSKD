using Domain;
using System.Collections.Generic;

namespace Application.Interfaces
{
  public interface IJwtGenerator
  {
    string CreateToken(AppUser user, IEnumerable<Roles> roles);
  }
}
