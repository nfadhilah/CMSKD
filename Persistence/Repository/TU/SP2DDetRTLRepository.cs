using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.TU;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;

namespace Persistence.Repository.TU
{
    public class SP2DDetRTLRepository : CommonRepository<SP2DDetRTL>
    {
      public SP2DDetRTLRepository(IDbConnection connection) : base(connection)
      {
      }

      public SP2DDetRTLRepository(IDbConnection connection, ISqlGenerator<SP2DDetRTL> sqlGenerator) : base(connection, sqlGenerator)
      {
      }
    }
}
