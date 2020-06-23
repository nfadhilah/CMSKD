using Domain.BUD;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.BUD
{
  public class SP2DDetBRepository : DapperRepository<SP2DDetB>
  {
    public SP2DDetBRepository(IDbConnection connection) : base(connection) { }
    public SP2DDetBRepository(IDbConnection connection, ISqlGenerator<SP2DDetB> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
