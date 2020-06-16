using System.Data;
using Domain.Auth;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.Auth
{
  public class AppUserRepository : DapperRepository<AppUser>
  {
    public AppUserRepository(IDbConnection connection) : base(connection) { }

    public AppUserRepository(
      IDbConnection connection, ISqlGenerator<AppUser> sqlGenerator) : base(
      connection, sqlGenerator)
    { }
  }
}