using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
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

