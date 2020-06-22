using Domain.TUBEND;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.TUBEND
{
  public class TBPLRepository : DapperRepository<TBPL>
  {
    public TBPLRepository(IDbConnection connection) : base(connection) { }
    public TBPLRepository(IDbConnection connection, ISqlGenerator<TBPL> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
