using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
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