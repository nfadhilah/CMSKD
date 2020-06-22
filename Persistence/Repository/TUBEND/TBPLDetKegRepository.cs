using Domain.TUBEND;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.TUBEND
{
  public class TBPLDetKegRepository : DapperRepository<TBPLDetKeg>
  {
    public TBPLDetKegRepository(IDbConnection connection) : base(connection) { }
    public TBPLDetKegRepository(IDbConnection connection, ISqlGenerator<TBPLDetKeg> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
