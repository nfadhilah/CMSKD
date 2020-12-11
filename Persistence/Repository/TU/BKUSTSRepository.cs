using System.Data;
using Domain.TU;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;

namespace Persistence.Repository.TU
{
  public class BKUSTSRepository : CommonRepository<BKUSTS>
  {
    public BKUSTSRepository(IDbConnection connection) : base(connection)
    {
    }

    public BKUSTSRepository(IDbConnection connection, ISqlGenerator<BKUSTS> sqlGenerator) : base(connection, sqlGenerator)
    {
    }
  }
}