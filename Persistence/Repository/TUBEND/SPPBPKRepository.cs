using Domain.TUBEND;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;
using System.Data;

namespace Persistence.Repository.TUBEND
{
  public class SPPBPKRepository : CommonRepository<SPPBPK>
  {
    public SPPBPKRepository(IDbConnection connection) : base(connection) { }
    public SPPBPKRepository(IDbConnection connection, ISqlGenerator<SPPBPK> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
