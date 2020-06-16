using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
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