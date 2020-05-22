using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
{
  public class MPgrmRepository : DapperRepository<MPgrm>
  {
    public MPgrmRepository(IDbConnection connection) : base(connection) { }

    public MPgrmRepository(
      IDbConnection connection, ISqlGenerator<MPgrm> sqlGenerator) : base(
      connection, sqlGenerator)
    { }
  }
}