using System.Data;
using Domain.PM;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;

namespace Persistence.Repository.PM
{
  public class DiskusiPaketRepository : CommonRepository<DiskusiPaket>
  {
    public DiskusiPaketRepository(IDbConnection connection) : base(connection)
    {
    }

    public DiskusiPaketRepository(IDbConnection connection, ISqlGenerator<DiskusiPaket> sqlGenerator) : base(connection,
      sqlGenerator)
    {
    }
  }
}