using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
{
  public class BkBKasRepository : DapperRepository<BkBKas>
  {
    public BkBKasRepository(IDbConnection connection) : base(connection)
    {
    }

    public BkBKasRepository(
      IDbConnection connection, ISqlGenerator<BkBKas> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }
  }
}