using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
{
  public class BendKPARepository : DapperRepository<BendKPA>
  {
    public BendKPARepository(IDbConnection connection) : base(connection) { }
    public BendKPARepository(IDbConnection connection, ISqlGenerator<BendKPA> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
