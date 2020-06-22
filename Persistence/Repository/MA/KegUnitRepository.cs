using System.Data;
using Domain.MA;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.MA
{
  public class KegUnitRepository : DapperRepository<KegUnit>
  {
    public KegUnitRepository(IDbConnection connection) : base(connection) { }

    public KegUnitRepository(
      IDbConnection connection, ISqlGenerator<KegUnit> sqlGenerator) : base(
      connection, sqlGenerator)
    { }
  }
}