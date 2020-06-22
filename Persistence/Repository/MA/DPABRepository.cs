using System.Data;
using Domain.MA;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.MA
{
  public class DPABRepository : DapperRepository<DPAB>
  {
    public DPABRepository(IDbConnection connection) : base(connection)
    {
    }

    public DPABRepository(
      IDbConnection connection, ISqlGenerator<DPAB> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }
  }
}