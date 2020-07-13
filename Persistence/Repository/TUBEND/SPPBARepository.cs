using Domain.TUBEND;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;
using System.Data;

namespace Persistence.Repository.TUBEND
{
  public class SPPBARepository : CommonRepository<SPPBA>
  {
    public SPPBARepository(IDbConnection connection) : base(connection) { }
    public SPPBARepository(IDbConnection connection, ISqlGenerator<SPPBA> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
