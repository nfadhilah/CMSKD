using Domain.TUBEND;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.TUBEND
{
  public class SPPDetRPRepository : DapperRepository<SPPDetRP>
  {
    public SPPDetRPRepository(IDbConnection connection) : base(connection) { }
    public SPPDetRPRepository(IDbConnection connection, ISqlGenerator<SPPDetRP> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
