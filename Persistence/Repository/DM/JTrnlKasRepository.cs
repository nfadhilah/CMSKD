using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.DM
{
  public class JTrnlKasRepository : DapperRepository<JTrnlKas>
  {
    public JTrnlKasRepository(IDbConnection connection) : base(connection) { }
    public JTrnlKasRepository(IDbConnection connection, ISqlGenerator<JTrnlKas> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
