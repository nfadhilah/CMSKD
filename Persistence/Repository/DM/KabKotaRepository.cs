using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;

namespace Persistence.Repository.DM
{
  public class KabKotaRepository : CommonRepository<KabKota>
  {
    public KabKotaRepository(IDbConnection connection) : base(connection) { }

    public KabKotaRepository(
      IDbConnection connection, ISqlGenerator<KabKota> sqlGenerator) : base(
      connection, sqlGenerator) { }
  }
}