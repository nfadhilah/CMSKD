using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DM;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;

namespace Persistence.Repository.DM
{
  public class PegawaiRepository : CommonRepository<Pegawai>
  {
    public PegawaiRepository(IDbConnection connection) : base(connection)
    {
    }

    public PegawaiRepository(IDbConnection connection, ISqlGenerator<Pegawai> sqlGenerator) : base(connection,
      sqlGenerator)
    {
    }
  }
}