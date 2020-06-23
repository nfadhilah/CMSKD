using Domain.BUD;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.BUD
{
  public class SP2DDetRRepository : DapperRepository<SP2DDetR>
  {
    public SP2DDetRRepository(IDbConnection connection) : base(connection) { }
    public SP2DDetRRepository(IDbConnection connection, ISqlGenerator<SP2DDetR> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
