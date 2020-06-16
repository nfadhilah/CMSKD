using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
{
  public class JSatuanRepository : DapperRepository<JSatuan>
  {
    public JSatuanRepository(IDbConnection connection) : base(connection)
    {
    }

    public JSatuanRepository(
      IDbConnection connection, ISqlGenerator<JSatuan> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }
  }
}