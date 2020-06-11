using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
{
  public class DaftBankRepository : DapperRepository<DaftBank>
  {
    public DaftBankRepository(IDbConnection connection) : base(connection)
    {
    }

    public DaftBankRepository(
      IDbConnection connection, ISqlGenerator<DaftBank> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }
  }
}