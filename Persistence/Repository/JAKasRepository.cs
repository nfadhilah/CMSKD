using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
{
  public class JAKasRepository : DapperRepository<JAKas>
  {
    public JAKasRepository(IDbConnection connection) : base(connection)
    {
    }

    public JAKasRepository(
      IDbConnection connection, ISqlGenerator<JAKas> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }
  }
}