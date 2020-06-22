using Domain.TUBEND;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.TUBEND
{
  public class KontrakRepository : DapperRepository<Kontrak>
  {
    public KontrakRepository(IDbConnection connection) : base(connection) { }
    public KontrakRepository(IDbConnection connection, ISqlGenerator<Kontrak> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
