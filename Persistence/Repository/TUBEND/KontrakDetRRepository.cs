using Domain.TUBEND;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;
using System.Data;

namespace Persistence.Repository.TUBEND
{
  public class KontrakDetRRepository : CommonRepository<KontrakDetR>
  {
    public KontrakDetRRepository(IDbConnection connection) : base(connection)
    {
    }

    public KontrakDetRRepository(IDbConnection connection, ISqlGenerator<KontrakDetR> sqlGenerator) : base(connection, sqlGenerator)
    {
    }
  }
}
