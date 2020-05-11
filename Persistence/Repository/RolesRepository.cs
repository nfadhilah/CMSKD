using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
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