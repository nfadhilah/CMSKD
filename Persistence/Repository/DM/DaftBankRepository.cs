using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
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