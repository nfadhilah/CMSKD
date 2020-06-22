using Domain.TUBEND;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.TUBEND
{
  public class BPKPajakStrRepository : DapperRepository<BPKPajakStr>
  {
    public BPKPajakStrRepository(IDbConnection connection) : base(connection) { }
    public BPKPajakStrRepository(IDbConnection connection, ISqlGenerator<BPKPajakStr> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}