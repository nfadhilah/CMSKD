using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
{
    public class StatTrsRepository : DapperRepository<StatTrs>
    {
        public StatTrsRepository(IDbConnection connection) : base(connection) { }

        public StatTrsRepository(
          IDbConnection connection, ISqlGenerator<StatTrs> sqlGenerator) : base(
          connection, sqlGenerator)
        { }
    }
}
