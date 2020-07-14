using Domain.DM;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;
using System.Data;

namespace Persistence.Repository.DM
{
  public class TahapRepository : CommonRepository<Tahap>
  {
    public TahapRepository(IDbConnection connection) : base(connection) { }
    public TahapRepository(IDbConnection connection, ISqlGenerator<Tahap> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
