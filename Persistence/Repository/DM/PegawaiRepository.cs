using Domain.DM;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;
using System.Data;

namespace Persistence.Repository.DM
{
  public class PegawaiRepository : CommonRepository<Pegawai>
  {
    public PegawaiRepository(IDbConnection connection) : base(connection)
    {
    }

    public PegawaiRepository(IDbConnection connection, ISqlGenerator<Pegawai> sqlGenerator) : base(connection,
      sqlGenerator)
    {
    }
  }
}