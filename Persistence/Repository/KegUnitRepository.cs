using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
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