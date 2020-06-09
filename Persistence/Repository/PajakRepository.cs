using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
{
  public class PajakRepository : DapperRepository<Pajak>
  {
    public PajakRepository(IDbConnection connection) : base(connection)
    {
    }

    public PajakRepository(
      IDbConnection connection, ISqlGenerator<Pajak> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }
  }
}