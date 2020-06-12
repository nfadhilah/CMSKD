using Dapper;
using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Persistence.Repository
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