using Domain.DM;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;
using System.Data;

namespace Persistence.Repository.DM
{
  public class DocMetaRepository : CommonRepository<DocMeta>
  {
    public DocMetaRepository(IDbConnection connection) : base(connection)
    {
    }

    public DocMetaRepository(IDbConnection connection, ISqlGenerator<DocMeta> sqlGenerator) : base(connection, sqlGenerator)
    {
    }
  }
}
