using Domain.TUBEND;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.TUBEND
{
  public class SPPDetBRepository : DapperRepository<SPPDetB>
  {
    public SPPDetBRepository(IDbConnection connection) : base(connection) { }
    public SPPDetBRepository(IDbConnection connection, ISqlGenerator<SPPDetB> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}