using Domain.TUBEND;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.TUBEND
{
  public class BeritaRepository : DapperRepository<Berita>
  {
    public BeritaRepository(IDbConnection connection) : base(connection) { }
    public BeritaRepository(IDbConnection connection, ISqlGenerator<Berita> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
