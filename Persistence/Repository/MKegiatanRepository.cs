using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
{
  public class MKegiatanRepository : DapperRepository<MKegiatan>
  {
    public MKegiatanRepository(IDbConnection connection) : base(connection) { }

    public MKegiatanRepository(
      IDbConnection connection, ISqlGenerator<MKegiatan> sqlGenerator) : base(
      connection, sqlGenerator)
    { }
  }
}