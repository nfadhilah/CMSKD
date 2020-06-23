using Domain.BUD;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.BUD
{
  public class DPDetRepository : DapperRepository<DPDet>
  {
    public DPDetRepository(IDbConnection connection) : base(connection) { }
    public DPDetRepository(IDbConnection connection, ISqlGenerator<DPDet> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
