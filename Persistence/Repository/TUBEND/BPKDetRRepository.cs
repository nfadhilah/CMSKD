using System.Data;
using Domain.TUBEND;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.TUBEND {
  public class BPKDetRRepository : DapperRepository<BPKDetR>
  {
    public BPKDetRRepository(IDbConnection connection) : base(connection) { }
    public BPKDetRRepository(IDbConnection connection, ISqlGenerator<BPKDetR> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}