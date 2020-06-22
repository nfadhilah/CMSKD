using Domain.TUBEND;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.TUBEND
{
  public class BkPajakRepository : DapperRepository<BkPajak>
  {
    public BkPajakRepository(IDbConnection connection) : base(connection) { }
    public BkPajakRepository(IDbConnection connection, ISqlGenerator<BkPajak> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}