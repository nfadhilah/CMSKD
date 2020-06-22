using System.Data;
using Domain.MA;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.MA
{
  public class DPADetRRepository : DapperRepository<DPADetR>
  {
    public DPADetRRepository(IDbConnection connection) : base(connection)
    {
    }

    public DPADetRRepository(
      IDbConnection connection, ISqlGenerator<DPADetR> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }
  }
}