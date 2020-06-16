using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
{
  public class DaftFungsiRepository : DapperRepository<DaftFungsi>
  {
    public DaftFungsiRepository(IDbConnection connection) : base(connection)
    {
    }

    public DaftFungsiRepository(
      IDbConnection connection, ISqlGenerator<DaftFungsi> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }
  }
}