using Domain.TUBEND;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.TUBEND
{
  public class BkPajakDetStrRepository : DapperRepository<BkPajakDetStr>
  {
    public BkPajakDetStrRepository(IDbConnection connection) : base(connection) { }
    public BkPajakDetStrRepository(IDbConnection connection, ISqlGenerator<BkPajakDetStr> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}