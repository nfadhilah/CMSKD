using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
{
    public class SifatKegRepository : DapperRepository<SifatKeg>
    {
        public SifatKegRepository(IDbConnection connection) : base(connection)
        {
        }

        public SifatKegRepository(
          IDbConnection connection, ISqlGenerator<SifatKeg> sqlGenerator) : base(
          connection, sqlGenerator)
        {
        }
    }
}
