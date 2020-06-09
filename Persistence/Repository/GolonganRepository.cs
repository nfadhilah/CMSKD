using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
{
    public class GolonganRepository : DapperRepository<Golongan>
    {
        public GolonganRepository(IDbConnection connection) : base(connection) { }

        public GolonganRepository(
          IDbConnection connection, ISqlGenerator<Golongan> sqlGenerator) : base(
          connection, sqlGenerator)
        { }
    }
}

