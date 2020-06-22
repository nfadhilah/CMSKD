using Domain.TUBEND;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.TUBEND
{
  public class STSDetDRepository : DapperRepository<STSDetD>
  {
    public STSDetDRepository(IDbConnection connection) : base(connection) { }
    public STSDetDRepository(IDbConnection connection, ISqlGenerator<STSDetD> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}