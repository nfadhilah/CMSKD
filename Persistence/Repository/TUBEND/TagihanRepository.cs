using Domain.TUBEND;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;
using System.Data;

namespace Persistence.Repository.TUBEND
{
  public class TagihanRepository : CommonRepository<Tagihan>
  {
    public TagihanRepository(IDbConnection connection) : base(connection)
    {
    }

    public TagihanRepository(IDbConnection connection, ISqlGenerator<Tagihan> sqlGenerator) : base(connection, sqlGenerator)
    {
    }
  }
}
