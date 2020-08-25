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
  public class PaketRUPDetRepository : CommonRepository<PaketRUPDet>
  {
    public PaketRUPDetRepository(IDbConnection connection) :
      base(connection) { }

    public PaketRUPDetRepository(
      IDbConnection connection, ISqlGenerator<PaketRUPDet> sqlGenerator) : base(
      connection, sqlGenerator) { }
  }
}