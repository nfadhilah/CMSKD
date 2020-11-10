using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Auth;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;

namespace Persistence.Repository.Auth
{
  public class WebOtorRepository : CommonRepository<WebOtor>
  {
    public WebOtorRepository(IDbConnection connection) : base(connection)
    {
    }

    public WebOtorRepository(IDbConnection connection, ISqlGenerator<WebOtor> sqlGenerator) : base(connection,
      sqlGenerator)
    {
    }
  }
}