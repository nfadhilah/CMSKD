using System.Data;
using Domain.Auth;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.Auth
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