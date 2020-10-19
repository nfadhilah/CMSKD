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
    public class WebUserRepository : CommonRepository<WebUser>
    {
      public WebUserRepository(IDbConnection connection) : base(connection)
      {
      }

      public WebUserRepository(IDbConnection connection, ISqlGenerator<WebUser> sqlGenerator) : base(connection, sqlGenerator)
      {
      }
    }
}
