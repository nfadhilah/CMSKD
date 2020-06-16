using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
{
  public class NrcBendRepository : DapperRepository<NrcBend>
  {
    public NrcBendRepository(IDbConnection connection) : base(connection) { }
    public NrcBendRepository(IDbConnection connection, ISqlGenerator<NrcBend> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
