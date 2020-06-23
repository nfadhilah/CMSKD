using Domain.BUD;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.BUD
{
  public class SP2DDetBDanaRepository : DapperRepository<SP2DDetBDana>
  {
    public SP2DDetBDanaRepository(IDbConnection connection) : base(connection) { }
    public SP2DDetBDanaRepository(IDbConnection connection, ISqlGenerator<SP2DDetBDana> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
