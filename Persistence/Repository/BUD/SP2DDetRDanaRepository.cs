using Domain.BUD;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.BUD
{
  public class SP2DDetRDanaRepository : DapperRepository<SP2DDetRDana>
  {
    public SP2DDetRDanaRepository(IDbConnection connection) : base(connection) { }
    public SP2DDetRDanaRepository(IDbConnection connection, ISqlGenerator<SP2DDetRDana> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
