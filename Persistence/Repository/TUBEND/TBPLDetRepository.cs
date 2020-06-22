using Domain.TUBEND;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.TUBEND
{
  public class TBPLDetRepository : DapperRepository<TBPLDet>
  {
    public TBPLDetRepository(IDbConnection connection) : base(connection) { }
    public TBPLDetRepository(IDbConnection connection, ISqlGenerator<TBPLDet> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}