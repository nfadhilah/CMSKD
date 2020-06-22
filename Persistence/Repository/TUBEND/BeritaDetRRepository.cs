using Domain.TUBEND;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.TUBEND
{
  public class BeritaDetRRepository : DapperRepository<BeritaDetR>
  {
    public BeritaDetRRepository(IDbConnection connection) : base(connection) { }
    public BeritaDetRRepository(IDbConnection connection, ISqlGenerator<BeritaDetR> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
