using System.Data;
using Domain.MA;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.MA
{
  public class DPADanaRRepository : DapperRepository<DPADanaR>
  {
    public DPADanaRRepository(IDbConnection connection) : base(connection)
    {
    }

    public DPADanaRRepository(
      IDbConnection connection, ISqlGenerator<DPADanaR> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }
  }
}