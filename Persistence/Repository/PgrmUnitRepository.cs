using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
{
  public class PgrmUnitRepository : DapperRepository<PgrmUnit>
  {
    public PgrmUnitRepository(IDbConnection connection) : base(connection) { }

    public PgrmUnitRepository(
      IDbConnection connection, ISqlGenerator<PgrmUnit> sqlGenerator) : base(
      connection, sqlGenerator)
    { }
  }
}