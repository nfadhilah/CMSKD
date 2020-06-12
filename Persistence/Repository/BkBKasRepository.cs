using Dapper;
using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Persistence.Repository
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