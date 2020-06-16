using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.Auth
{
  public class RolesRepository : DapperRepository<Roles>
  {
    public RolesRepository(IDbConnection connection) : base(connection) { }

    public RolesRepository(
      IDbConnection connection, ISqlGenerator<Roles> sqlGenerator) : base(
      connection, sqlGenerator)
    { }
  }
}