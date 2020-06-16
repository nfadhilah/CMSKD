using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
{
  public class BendKPARepository : DapperRepository<BendKPA>
  {
    public BendKPARepository(IDbConnection connection) : base(connection) { }
    public BendKPARepository(IDbConnection connection, ISqlGenerator<BendKPA> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
