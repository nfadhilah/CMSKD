using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
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