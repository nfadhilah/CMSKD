using System.Data;
using Domain.TUBEND;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.TUBEND {
  public class STSDetBRepository : DapperRepository<STSDetB>
  {
    public STSDetBRepository(IDbConnection connection) : base(connection) { }
    public STSDetBRepository(IDbConnection connection, ISqlGenerator<STSDetB> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}