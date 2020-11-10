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
  public class TahunRepository : CommonRepository<Tahun>

  {
    public TahunRepository(IDbConnection connection) : base(connection)
    {
    }

    public TahunRepository(IDbConnection connection, ISqlGenerator<Tahun> sqlGenerator) : base(connection, sqlGenerator)
    {
    }
  }
}