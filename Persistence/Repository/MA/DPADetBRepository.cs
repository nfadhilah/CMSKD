using System.Data;
using Domain.MA;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.MA
{
  public class DPADetBRepository : DapperRepository<DPADetB>
  {
    public DPADetBRepository(IDbConnection connection) : base(connection)
    {
    }

    public DPADetBRepository(
      IDbConnection connection, ISqlGenerator<DPADetB> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }
  }
}