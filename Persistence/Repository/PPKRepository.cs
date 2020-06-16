using Dapper;
using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Persistence.Repository
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