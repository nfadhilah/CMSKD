using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
{
  public class BendRepository : DapperRepository<Bend>
  {
    public BendRepository(IDbConnection connection) : base(connection)
    {
    }

    public BendRepository(
      IDbConnection connection, ISqlGenerator<Bend> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }
  }
}