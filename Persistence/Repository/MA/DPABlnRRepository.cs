using System.Data;
using Domain.MA;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.MA
{
  public class DPABlnRRepository : DapperRepository<DPABlnR>
  {
    public DPABlnRRepository(IDbConnection connection) : base(connection)
    {
    }

    public DPABlnRRepository(
      IDbConnection connection, ISqlGenerator<DPABlnR> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }
  }
}