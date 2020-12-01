using Domain.TUBEND;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;
using System.Data;

namespace Persistence.Repository.TUBEND
{
  public class TagihanDetRepository : CommonRepository<TagihanDet>
  {
    public TagihanDetRepository(IDbConnection connection) : base(connection)
    {
    }

    public TagihanDetRepository(IDbConnection connection, ISqlGenerator<TagihanDet> sqlGenerator) : base(connection, sqlGenerator)
    {
    }
  }
}