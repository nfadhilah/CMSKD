using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
{
  public class TahunRepository : DapperRepository<Tahun>
  {
    public TahunRepository(IDbConnection connection) : base(connection) { }

    public TahunRepository(
      IDbConnection connection, ISqlGenerator<Tahun> sqlGenerator) : base(
      connection, sqlGenerator)
    { }
  }
}