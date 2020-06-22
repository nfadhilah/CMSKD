using Domain.TUBEND;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.TUBEND
{
  public class SPPDetRDanaRepository : DapperRepository<SPPDetRDana>
  {
    public SPPDetRDanaRepository(IDbConnection connection) : base(connection) { }
    public SPPDetRDanaRepository(IDbConnection connection, ISqlGenerator<SPPDetRDana> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
