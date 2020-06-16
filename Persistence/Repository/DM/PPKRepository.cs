using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
{
  public class PPKRepository : DapperRepository<PPK>
  {
    public PPKRepository(IDbConnection connection) : base(connection)
    {
    }

    public PPKRepository(
      IDbConnection connection, ISqlGenerator<PPK> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }
  }
}