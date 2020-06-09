using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
{
  public class UrusanUnitRepository : DapperRepository<UrusanUnit>
  {
    public UrusanUnitRepository(IDbConnection connection) : base(connection) { }
    public UrusanUnitRepository(IDbConnection connection, ISqlGenerator<UrusanUnit> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
