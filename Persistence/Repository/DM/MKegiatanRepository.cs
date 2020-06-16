using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
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