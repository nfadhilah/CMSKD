using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
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