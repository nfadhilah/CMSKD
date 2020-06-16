using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
{
  public class DaftPhk3Repository : DapperRepository<DaftPhk3>
  {
    public DaftPhk3Repository(IDbConnection connection) : base(connection)
    {
    }

    public DaftPhk3Repository(
      IDbConnection connection, ISqlGenerator<DaftPhk3> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }
  }
}