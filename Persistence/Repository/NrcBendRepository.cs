using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
{
  public class NrcBendRepository : DapperRepository<NrcBend>
  {
    public NrcBendRepository(IDbConnection connection) : base(connection) { }
    public NrcBendRepository(IDbConnection connection, ISqlGenerator<NrcBend> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
