using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
{
  public class PegawaiRepository : DapperRepository<Pegawai>
  {
    public PegawaiRepository(IDbConnection connection) : base(connection)
    {
    }

    public PegawaiRepository(
      IDbConnection connection, ISqlGenerator<Pegawai> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }
  }
}