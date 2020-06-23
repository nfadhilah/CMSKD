using Domain.BUD;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.BUD
{
  public class SP2DDetRPRepository : DapperRepository<SP2DDetRP>
  {
    public SP2DDetRPRepository(IDbConnection connection) : base(connection) { }
    public SP2DDetRPRepository(IDbConnection connection, ISqlGenerator<SP2DDetRP> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
