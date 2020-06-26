using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.DM
{
  public class WebSetRepository : DapperRepository<WebSet>
  {
    public WebSetRepository(IDbConnection connection) : base(connection) { }
    public WebSetRepository(IDbConnection connection, ISqlGenerator<WebSet> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
