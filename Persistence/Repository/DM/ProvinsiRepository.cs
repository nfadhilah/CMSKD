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
  public class ProvinsiRepository : CommonRepository<Provinsi>
  {
    public ProvinsiRepository(IDbConnection connection) : base(connection) { }

    public ProvinsiRepository(
      IDbConnection connection, ISqlGenerator<Provinsi> sqlGenerator) : base(
      connection, sqlGenerator) { }
  }
}