using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
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