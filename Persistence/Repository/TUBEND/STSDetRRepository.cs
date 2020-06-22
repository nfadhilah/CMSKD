using Domain.TUBEND;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.TUBEND
{
  public class STSDetRRepository : DapperRepository<STSDetR>
  {
    public STSDetRRepository(IDbConnection connection) : base(connection) { }
    public STSDetRRepository(IDbConnection connection, ISqlGenerator<STSDetR> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}