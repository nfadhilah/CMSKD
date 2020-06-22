using System.Data;
using Domain.TUBEND;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.TUBEND {
  public class BPKDetRPRepository : DapperRepository<BPKDetRP>
  {
    public BPKDetRPRepository(IDbConnection connection) : base(connection) { }
    public BPKDetRPRepository(IDbConnection connection, ISqlGenerator<BPKDetRP> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}