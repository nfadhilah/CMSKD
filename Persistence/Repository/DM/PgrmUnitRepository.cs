using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
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