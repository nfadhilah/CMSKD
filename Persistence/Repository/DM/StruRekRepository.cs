using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
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
