using System.Data;
using Domain.MA;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.MA
{
  public class DPADanaBRepository : DapperRepository<DPADanaB>
  {
    public DPADanaBRepository(IDbConnection connection) : base(connection)
    {
    }

    public DPADanaBRepository(
      IDbConnection connection, ISqlGenerator<DPADanaB> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }
  }
}