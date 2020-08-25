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
  public class JPekerjaanRepository : CommonRepository<JPekerjaan>
  {
    public JPekerjaanRepository(IDbConnection connection) : base(connection) { }

    public JPekerjaanRepository(
      IDbConnection connection, ISqlGenerator<JPekerjaan> sqlGenerator) : base(
      connection, sqlGenerator) { }
  }
}