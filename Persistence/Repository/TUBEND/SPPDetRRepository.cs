using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;
using Domain.TUBEND;

namespace Persistence.Repository.TUBEND
{
  public class SPPDetRRepository : DapperRepository<SPPDetR>
  {
    public SPPDetRRepository(IDbConnection connection) : base(connection) { }
    public SPPDetRRepository(IDbConnection connection, ISqlGenerator<SPPDetR> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}