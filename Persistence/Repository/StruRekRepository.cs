using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
{
    public class StruRekRepository : DapperRepository<StruRek>
    {
        public StruRekRepository(IDbConnection connection) : base(connection) { }

        public StruRekRepository(
          IDbConnection connection, ISqlGenerator<StruRek> sqlGenerator) : base(
          connection, sqlGenerator)
        { }
    }
}
