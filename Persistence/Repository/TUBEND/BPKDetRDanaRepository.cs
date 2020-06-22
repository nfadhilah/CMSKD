using System.Data;
using Domain.TUBEND;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.TUBEND {
  public class BPKDetRDanaRepository : DapperRepository<BPKDetRDana>
  {
    public BPKDetRDanaRepository(IDbConnection connection) : base(connection) { }
    public BPKDetRDanaRepository(IDbConnection connection, ISqlGenerator<BPKDetRDana> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}