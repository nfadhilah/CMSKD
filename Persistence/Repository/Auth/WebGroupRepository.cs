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
  public class WebGroupRepository : CommonRepository<WebGroup>
  {
    public WebGroupRepository(IDbConnection connection) : base(connection)
    {
    }

    public WebGroupRepository(IDbConnection connection, ISqlGenerator<WebGroup> sqlGenerator) : base(connection,
      sqlGenerator)
    {
    }
  }
}