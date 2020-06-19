using System.Data;
using Domain.MA;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.MA
{
  public class DPARepository : DapperRepository<DPA>
  {
    public DPARepository(IDbConnection connection) : base(connection)
    {
    }

    public DPARepository(
      IDbConnection connection, ISqlGenerator<DPA> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }
  }
}