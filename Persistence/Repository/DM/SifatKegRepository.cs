using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
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
