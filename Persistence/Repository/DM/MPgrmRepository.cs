using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
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