using System.Data;
using Domain.TU;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;

namespace Persistence.Repository.TU
{
  public class RkmDetDRepository : CommonRepository<RkmDetD>
  {
    public RkmDetDRepository(IDbConnection connection) : base(connection)
    {
    }

    public RkmDetDRepository(IDbConnection connection, ISqlGenerator<RkmDetD> sqlGenerator) : base(connection, sqlGenerator)
    {
    }
  }
}