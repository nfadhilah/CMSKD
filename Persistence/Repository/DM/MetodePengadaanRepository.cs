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
  public class MetodePengadaanRepository : CommonRepository<MetodePengadaan>
  {
    public MetodePengadaanRepository(IDbConnection connection) : base(
      connection) { }

    public MetodePengadaanRepository(
      IDbConnection connection, ISqlGenerator<MetodePengadaan> sqlGenerator) :
      base(connection, sqlGenerator) { }
  }
}