using System.Data;
using Domain.MA;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.MA
{
  public class DPARRepository : DapperRepository<DPAR>
  {
    public DPARRepository(IDbConnection connection) : base(connection)
    {
    }

    public DPARRepository(
      IDbConnection connection, ISqlGenerator<DPAR> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }
  }
}