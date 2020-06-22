using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.DM
{
  public class JTahunRepository : DapperRepository<JTahun>
  {
    public JTahunRepository(IDbConnection connection) : base(connection) { }

    public JTahunRepository(
      IDbConnection connection, ISqlGenerator<JTahun> sqlGenerator) : base(
      connection, sqlGenerator)
    { }
  }
}