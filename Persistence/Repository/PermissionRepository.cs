using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
{
  public class PermissionRepository : DapperRepository<Permission>

  {
    public PermissionRepository(IDbConnection connection) : base(connection) { }

    public PermissionRepository(
      IDbConnection connection, ISqlGenerator<Permission> sqlGenerator) : base(
      connection, sqlGenerator)
    { }
  }
}