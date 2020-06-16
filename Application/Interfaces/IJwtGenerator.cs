using Domain;
using System.Collections.Generic;
using Domain.Auth;
using Domain.DM;

namespace Application.Interfaces
{
  public interface IJwtGenerator
  {
    string CreateToken(AppUser user, IEnumerable<Roles> roles);
  }
}
