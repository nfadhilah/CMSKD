using Domain.TUBEND;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.TUBEND
{
  public class SPPDetBDanaRepository : DapperRepository<SPPDetBDana>
  {
    public SPPDetBDanaRepository(IDbConnection connection) : base(connection) { }
    public SPPDetBDanaRepository(IDbConnection connection, ISqlGenerator<SPPDetBDana> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
