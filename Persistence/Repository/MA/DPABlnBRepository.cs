using System.Data;
using Domain.MA;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.MA
{
  public class DPABlnBRepository : DapperRepository<DPABlnB>
  {
    public DPABlnBRepository(IDbConnection connection) : base(connection)
    {
    }

    public DPABlnBRepository(
      IDbConnection connection, ISqlGenerator<DPABlnB> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }
  }
}