using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
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
