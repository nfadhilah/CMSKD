using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
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