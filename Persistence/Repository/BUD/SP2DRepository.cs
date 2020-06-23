using Domain.BUD;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.BUD
{
  public class SP2DRepository : DapperRepository<SP2D>
  {
    public SP2DRepository(IDbConnection connection) : base(connection) { }
    public SP2DRepository(IDbConnection connection, ISqlGenerator<SP2D> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
