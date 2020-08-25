using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.PM;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;

namespace Persistence.Repository.PM
{
  public class PaketRUPRepository : CommonRepository<PaketRUP>
  {
    public PaketRUPRepository(IDbConnection connection) : base(connection) { }

    public PaketRUPRepository(
      IDbConnection connection, ISqlGenerator<PaketRUP> sqlGenerator) : base(
      connection, sqlGenerator) { }
  }
}