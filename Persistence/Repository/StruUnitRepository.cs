using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
{
    public class StruUnitRepository : DapperRepository<StruUnit>
    {
        public StruUnitRepository(IDbConnection connection) : base(connection) { }

        public StruUnitRepository(
          IDbConnection connection, ISqlGenerator<StruUnit> sqlGenerator) : base(
          connection, sqlGenerator)
        { }
    }
}
