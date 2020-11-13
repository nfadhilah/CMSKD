using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Domain.DM;
using Domain.TU;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;

namespace Persistence.Repository.TU
{
  public class SP2DRepository : CommonRepository<SP2D>
  {
    public SP2DRepository(IDbConnection connection) : base(connection)
    {
    }

    public SP2DRepository(IDbConnection connection, ISqlGenerator<SP2D> sqlGenerator) : base(connection, sqlGenerator)
    {
    }
  }
}