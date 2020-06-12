using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
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
