using Domain.TU;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;
using System.Data;

namespace Persistence.Repository.TU
{
  public class BKUDRepository : CommonRepository<BKUD>
  {
    public BKUDRepository(IDbConnection connection) : base(connection)
    {
    }

    public BKUDRepository(IDbConnection connection, ISqlGenerator<BKUD> sqlGenerator) : base(connection, sqlGenerator)
    {
    }
  }
}