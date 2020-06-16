using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
{
  public class DaftRekeningRepository : DapperRepository<DaftRekening>
  {
    public DaftRekeningRepository(IDbConnection connection) : base(connection) { }
    public DaftRekeningRepository(IDbConnection connection, ISqlGenerator<DaftRekening> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
