using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
{
  public class UrusanUnitRepository : DapperRepository<UrusanUnit>
  {
    public UrusanUnitRepository(IDbConnection connection) : base(connection) { }
    public UrusanUnitRepository(IDbConnection connection, ISqlGenerator<UrusanUnit> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
