using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
{
  public class DaftRekeningRepository : DapperRepository<DaftRekening>
  {
    public DaftRekeningRepository(IDbConnection connection) : base(connection) { }
    public DaftRekeningRepository(IDbConnection connection, ISqlGenerator<DaftRekening> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
